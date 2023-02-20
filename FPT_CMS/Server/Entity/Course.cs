using System.ComponentModel.DataAnnotations;

namespace Server.Entity
{
    public class Course
    {
        public Course()
        {
            Grades = new List<Grade>();
        }
        public string CourseId { get; set; }
        public int Slot { get; set; }
        public string SemesterId { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
