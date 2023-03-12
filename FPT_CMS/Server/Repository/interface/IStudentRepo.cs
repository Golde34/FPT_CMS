using Server.Entity;

namespace Server.Repository.@interface
{
    public interface IStudentRepo
    {
        Student AddStudent(Student Student);
        Student UpdateStudent(Student Student);
        Student DeleteStudent(Student Student);
        IEnumerable<Student> GetStudents();
        Student GetStudentById(int? id);
        Student GetStudentByAccountId(string id);
    }
}
