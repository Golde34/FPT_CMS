namespace Server.Entity
{
    public class Notification
    {
        public Notification()
        {
            Comments = new List<Comment>();
        }
        public int NotificationId { get; set; }
        public string Text { get; set; }
        public string UploadFile { get; set; }
        public string CourseId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Course? Course { get; set; }
    }
}
