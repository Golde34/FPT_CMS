using System.ComponentModel.DataAnnotations;

namespace Server.Entity
{
    public class Subject
    {
        public Subject() {
            Courses = new List<Course>();
        }
        [Key]
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public decimal Fee { get; set; }
        public int Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public ICollection<CurriculumDetail> Details { get; set; }
    }
}
