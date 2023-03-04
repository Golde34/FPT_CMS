using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class SemesterRepository : ISemesterRepo
    {
        Semester ISemesterRepo.AddSemester(Semester Semester) => SemesterManagement.Instance.AddSemester(Semester);
        Semester ISemesterRepo.UpdateSemester(Semester Semester) => SemesterManagement.Instance.UpdateSemester(Semester);
        Semester ISemesterRepo.DeleteSemester(Semester Semester) => SemesterManagement.Instance.DeleteSemester(Semester);
        IEnumerable<Semester> ISemesterRepo.GetSemesters() => SemesterManagement.Instance.GetSemesters();
        Semester ISemesterRepo.GetSemesterById(string? id) => SemesterManagement.Instance.GetSemesterById(id);
    }
}
