using Server.Entity;

namespace Server.Repository.@interface
{
    public interface INotificationRepo
    {
        Notification AddNotification(Notification Notification);
        Notification UpdateNotification(Notification Notification);
        Notification DeleteNotification(Notification Notification);
        IEnumerable<Notification> GetNotifications();
        Notification GetNotificationById(int? id);
    }
}
