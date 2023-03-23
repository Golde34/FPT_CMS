using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class NotificationRepository : INotificationRepo
    {
        Notification INotificationRepo.AddNotification(Notification Notification) => NotificationManagement.Instance.AddNotification(Notification);
        Notification INotificationRepo.UpdateNotification(Notification Notification) => NotificationManagement.Instance.UpdateNotification(Notification);
        Notification INotificationRepo.DeleteNotification(Notification Notification) => NotificationManagement.Instance.DeleteNotification(Notification);
        IEnumerable<Notification> INotificationRepo.GetNotifications(string courseId) => NotificationManagement.Instance.GetNotifications(courseId);
        Notification INotificationRepo.GetNotificationById(int? id) => NotificationManagement.Instance.GetNotificationById(id);
        Notification INotificationRepo.GetNotificationByTopicId(string? accountId, string? courseid) => NotificationManagement.Instance.GetNotificationByTopicId(accountId, courseid);
    }
}
