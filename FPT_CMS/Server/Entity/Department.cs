namespace Server.Entity
{
    public class Department
    {
        public Department()
        {
            Subjects = new List<Subject>();
        }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Subject> Subjects { get; set; }

    }
}
