using Server.Entity.Enum;

namespace Server.Entity
{
    public class Account
    {
        public Account() 
        {
            Notifications = new List<Notification>();
            Comments= new List<Comment>();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
