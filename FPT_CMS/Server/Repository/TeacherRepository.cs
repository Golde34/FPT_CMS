using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class TeacherRepository : ITeacherRepo
    {
        Teacher ITeacherRepo.AddTeacher(Teacher Teacher) => TeacherManagement.Instance.AddTeacher(Teacher);
        Teacher ITeacherRepo.UpdateTeacher(Teacher Teacher) => TeacherManagement.Instance.UpdateTeacher(Teacher);
        Teacher ITeacherRepo.DeleteTeacher(Teacher Teacher) => TeacherManagement.Instance.DeleteTeacher(Teacher);
        IEnumerable<Teacher> ITeacherRepo.GetTeachers() => TeacherManagement.Instance.GetTeachers();
        Teacher ITeacherRepo.GetTeacherById(string? id) => TeacherManagement.Instance.GetTeacherById(id);

        public Teacher GetTeacherByAccountId(string? id) => TeacherManagement.Instance.GetTeacherByAccountId(id);
    }
}
