using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Xml.Linq;
using Server.DTO;
using Microsoft.Extensions.Primitives;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;
using Server.Repository;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class NotificationController : Controller
{
    private readonly IWebHostEnvironment _env;
    private INotificationRepo notificationRepo = new NotificationRepository();

    public NotificationController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<List<Notification>> GetNotifications(string id)
    {
        List<Notification> _Notifications;
        string rootPath = _env.WebRootPath + "\\Notification\\";
        try
        {
            var _NotificationManagement = new NotificationManagement();
            _Notifications = _NotificationManagement.GetNotifications(id).ToList();
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
                UploadFile = path,
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
    public ActionResult<List<Comment>> GetComments(string id)
    {
        List<Comment> _Comments;
        try
        {
            var _CommentManagement = new CommentManagement();
            _Comments = (List<Comment>)_CommentManagement.GetCommentsByNotification(int.Parse(id));

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return _Comments;
    }
}