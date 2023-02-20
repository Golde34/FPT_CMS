namespace Server.Entity
{
    public class Semester
    {
        public Semester()
        {
            Courses = new List<Course>();
        }
        public string SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
