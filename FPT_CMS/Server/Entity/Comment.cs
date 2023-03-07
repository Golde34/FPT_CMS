namespace Server.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public int NotificationID { get; set; }
        public string Text { get; set; }
    }
}
