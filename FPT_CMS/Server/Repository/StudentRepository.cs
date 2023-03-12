using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class StudentRepository : IStudentRepo
    {
        Student IStudentRepo.AddStudent(Student Student) => StudentManagement.Instance.AddStudent(Student);
        Student IStudentRepo.UpdateStudent(Student Student) => StudentManagement.Instance.UpdateStudent(Student);
        Student IStudentRepo.DeleteStudent(Student Student) => StudentManagement.Instance.DeleteStudent(Student);
        IEnumerable<Student> IStudentRepo.GetStudents() => StudentManagement.Instance.GetStudents();
        Student IStudentRepo.GetStudentById(int? id) => StudentManagement.Instance.GetStudentById(id);
        Student IStudentRepo.GetStudentByAccountId(string id) => StudentManagement.Instance.GetStudentByAccountId(id);
    }
}
