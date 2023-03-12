namespace Server.Entity
{
    public class Teacher
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccountId { get; set; }
        public virtual Account? Account { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}