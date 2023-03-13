using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Xml.Linq;
using Server.DTO;

[Authorize]
[Route("api/[controller]/[action]/{id}")]
[ApiController]
public class NotificationController : Controller
{
	[HttpGet]
	public ActionResult<List<Notification>> GetNotifications(string id)
	{
		List<Notification> _Notifications;
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

	public async Task<IActionResult> AddNotification(NotificationDTO notificationDTO, [FromServices] IHostingEnvironment hostingEnvironment) 
	{
        if (notificationDTO == null)
        {
            return BadRequest();
        }
        string uniqueFileName = null;
        if (notificationDTO.UploadFile != null)
        {
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "files");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + notificationDTO.UploadFile.FileName;
            using (var fs = new FileStream(Path.Combine(uploadsFolder, uniqueFileName), FileMode.Create))
            {
                await notificationDTO.UploadFile.CopyToAsync(fs);
            }
        }
        Notification noti = new Notification
        {
            Text = notificationDTO.Text,
            UploadFile = uniqueFileName,
            CourseId = notificationDTO.CourseId,
            AccountId = notificationDTO.AccountId
        };
        var notificationManagement = new NotificationManagement();
        notificationManagement.AddNotification(noti);
        return Ok();
    }
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