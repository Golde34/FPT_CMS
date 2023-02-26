namespace Server.Entity
{
    public class Teacher
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Account Account { get; set; }
    }
}
