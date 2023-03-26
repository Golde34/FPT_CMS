using System.ComponentModel.DataAnnotations;

namespace Server.Entity
{
    public class Course
    {
        public Course()
        {
            Grades = new List<Grade>();
            Topics = new List<Topic>();
            Notifications= new List<Notification>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int Slot { get; set; }
        public string SemesterId { get; set; }
        public string? SubjectCode { get; set; }
        public string? TeacherId { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Semester? Semester { get; set; }
    }
}