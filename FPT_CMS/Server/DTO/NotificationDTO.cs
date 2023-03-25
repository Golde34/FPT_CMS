using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Server.DTO
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public string Text { get; set; }
        public string UploadFile { get; set; }
        public string CourseId { get; set; }
        public string AccountId { get; set; }
        public string Username { get; set; }
    }
}
