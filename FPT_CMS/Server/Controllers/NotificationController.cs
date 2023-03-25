using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;
using Microsoft.Extensions.Primitives;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;
using Server.Repository;
using Server.DTO;
using Server.Entity.Enum;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class NotificationController : Controller
{
    private readonly IWebHostEnvironment _env;
    private INotificationRepo notificationRepo = new NotificationRepository();
    private ICommentRepo commentRepo = new CommentRepository();
    private ITeacherRepo teacherRepo = new TeacherRepository();
    private IStudentRepo studentRepo = new StudentRepository();

    public NotificationController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<List<NotificationDTO>> GetNotifications(string id)
    {
        List<NotificationDTO> _Notifications = new List<NotificationDTO>();
        try
        {
            var _NotificationManagement = new NotificationManagement();
            List<Notification> noti = _NotificationManagement.GetNotifications(id).ToList();
            foreach (var n in noti)
            {
                if (n.Account.Role == Roles.Teacher)
                {
                    NotificationDTO notiDTO = new NotificationDTO
                    {
                        NotificationId = n.NotificationId,
                        Text = n.Text,
                        UploadFile = n.UploadFile,
                        CourseId = n.CourseId,
                        AccountId = n.AccountId,
                        Username = teacherRepo.GetTeacherByAccountId(n.AccountId).Name,
                    };
                    _Notifications.Add(notiDTO);
                } else if (n.Account.Role == Roles.Student)
                {
                    NotificationDTO notiDTO = new NotificationDTO
                    {
                        NotificationId = n.NotificationId,
                        Text = n.Text,
                        UploadFile = n.UploadFile,
                        CourseId = n.CourseId,
                        AccountId = n.AccountId,
                        Username = studentRepo.GetStudentByAccountId(n.AccountId).StudentName,
                    };
                    _Notifications.Add(notiDTO);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return _Notifications;
    }

    public async Task<IActionResult> AddNotification([FromForm] IFormFile file, [FromForm] string CourseId, [FromForm] string AccountId, [FromForm] string Text)
    {
        var webRootPath = _env.WebRootPath;
        if (!Directory.Exists(webRootPath + "\\Notification\\"))
        {
            Directory.CreateDirectory(webRootPath + "\\Notification\\");
        }

        // Decode the token and get the role of account
        StringValues values;
        Request.Headers.TryGetValue("Authorization", out values);
        var token = values.ToString();
        string[] tokens = token.Split(" ");

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
        var id = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

        int notificationListLength = notificationRepo.GetNotifications(CourseId).ToList().Count()+1;
        if (file != null)
        {
            string path = webRootPath + "\\Notification\\" + file.FileName;
            using (FileStream fileStream = System.IO.File.Create(path))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            Notification notification = new Notification
            {
                Text = Text,
                UploadFile = file.FileName,
                CourseId = CourseId,
                AccountId = AccountId
            };
            var notificationManagement = new NotificationManagement();
            notificationManagement.AddNotification(notification);
        }
        else
        {
            Notification notification = new Notification
            {
                Text = Text,
                UploadFile = "",
                CourseId = CourseId,
                AccountId = AccountId
            };
            var notificationManagement = new NotificationManagement();
            notificationManagement.AddNotification(notification);
        }
        return Ok();
    }

    [Route("{id}")]
    public ActionResult<List<CommentDTO>> GetComments(string id)
    {
        List<CommentDTO> commentsDTO = new List<CommentDTO>();
        try
        {
            var _CommentManagement = new CommentManagement();
            var comments = (List<Comment>)_CommentManagement.GetCommentsByNotification(int.Parse(id));
            foreach (var c in comments)
            {
                if (c.Account.Role == Roles.Teacher)
                {
                    CommentDTO commentDTO = new CommentDTO
                    {
                        Id = c.Id,
                        AccountId = c.AccountId,
                        NotificationID= c.NotificationID,
                        Text = c.Text,
                        Username = teacherRepo.GetTeacherByAccountId(c.AccountId).Name,
                    };
                    commentsDTO.Add(commentDTO);
                }
                else if (c.Account.Role == Roles.Student)
                {
                    CommentDTO commentDTO = new CommentDTO
                    {
                        Id = c.Id,
                        AccountId = c.AccountId,
                        NotificationID = c.NotificationID,
                        Text = c.Text,
                        Username = studentRepo.GetStudentByAccountId(c.AccountId).StudentName,
                    };
                    commentsDTO.Add(commentDTO);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return commentsDTO;
    }

    public async Task<IActionResult> AddComment(CommentDTO commentDTO)
    {
        Comment comment = new Comment
        {
            Text = commentDTO.Text,
            AccountId = commentDTO.AccountId,
            NotificationID = commentDTO.NotificationID
        };
        commentRepo.AddComment(comment);
        return Ok();
    }
}