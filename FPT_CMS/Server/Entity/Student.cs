using Server.Entity.Enum;

namespace Server.Entity
{
    public class Student
    {
        public Student() { 
            Grades = new List<Grade>();
            Submissions = new List<Submission>();
        }
        public int Id { get; set; }
        public string StudentRollNumber { get; set; }
        public string StudentName { get; set; }
        public StudentMajors Majors { get; set; }
        public int CurriculumId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Account Account { get; set; }
        public virtual ICollection<Grade> Grades { get; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
