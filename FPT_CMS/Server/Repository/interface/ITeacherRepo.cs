using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ITeacherRepo
    {
        Teacher AddTeacher(Teacher Teacher);
        Teacher UpdateTeacher(Teacher Teacher);
        Teacher DeleteTeacher(Teacher Teacher);
        IEnumerable<Teacher> GetTeachers();
        Teacher GetTeacherById(string? id);
        Teacher GetTeacherByAccountId(string? id);
    }
}
