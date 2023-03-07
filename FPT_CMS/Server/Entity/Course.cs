using System.ComponentModel.DataAnnotations;

namespace Server.Entity
{
    public class Course
    {
        public Course()
        {
            Grades = new List<Grade>();
            Topics = new List<Topic>();
        }
        public string CourseId { get; set; }
        public int Slot { get; set; }
        public string SemesterId { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
