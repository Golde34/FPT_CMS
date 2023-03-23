using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class NotificationManagement
    {
        private static NotificationManagement instance;
        private static readonly object instancelock = new object();

        public NotificationManagement() { }

        public static NotificationManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new NotificationManagement();
                }
                return instance;
            }
        }

        public Notification AddNotification(Notification Notification)
        {
            try
            {
                Notification _Notification = GetNotificationById(Notification.NotificationId);
                if (_Notification == null)
                {
                    var context = new AppDBContext();
                    context.Notifications.Add(Notification);
                    context.SaveChanges();
                    return Notification;
                }
                else
                {
                    throw new Exception("The Notification's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Notification UpdateNotification(Notification Notification)
        {
            try
            {
                Notification _Notification = GetNotificationById(Notification.NotificationId);
                if (_Notification != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Notification>(Notification).State = EntityState.Modified;
                    context.SaveChanges();
                    return Notification;
                }
                else
                {
                    throw new Exception("The Notification is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Notification DeleteNotification(Notification Notification)
        {
            try
            {
                Notification _Notification = GetNotificationById(Notification.NotificationId);
                if (_Notification != null)
                {
                    var context = new AppDBContext();
                    context.Notifications.Remove(_Notification);
                    context.SaveChanges();
                    return _Notification;
                }
                else
                {
                    throw new Exception("The Notification is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Notification> GetNotifications(string courseId)
        {
            List<Notification> Notifications;
            try
            {
                var context = new AppDBContext();
                Notifications = context.Notifications.Where(x => x.CourseId == courseId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Notifications;
        }

        public Notification GetNotificationById(int? id)
        {
            Notification Notification;
            try
            {
                var _dbContext = new AppDBContext();
                Notification = _dbContext.Notifications
                    .FirstOrDefault(a => a.NotificationId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Notification;
        }

        public Notification GetNotificationByTopicId(string? accountId, string? courseId)
        {
            Notification Notification;
            try
            {
                var _dbContext = new AppDBContext();
                Notification = _dbContext.Notifications.Include(a => a.Account).Include(c => c.Course)
                    .FirstOrDefault(t => t.AccountId== accountId && t.CourseId == courseId);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Notification;
        }
    }
}
