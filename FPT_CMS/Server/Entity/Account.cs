using Server.Entity.Enum;

namespace Server.Entity
{
    public class Account
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
