using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;
using System.Xml.Linq;

[Authorize]
[Route("api/[controller]/[action]/{id}")]
[ApiController]
public class NotificationController
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