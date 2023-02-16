using Server.Entity.Enum;

namespace Server.Entity
{
    public class Curriculum
    {
        public Curriculum ()
        {
            Students = new List<Student> ();
        }
        public int CurriculumId { get; set; }
        public string CurriculumName { get; set; }
        public StudentMajors Majors { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
