using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ISemesterRepo
    {
        Semester AddSemester(Semester Semester);
        Semester UpdateSemester(Semester Semester);
        Semester DeleteSemester(Semester Semester);
        IEnumerable<Semester> GetSemesters();
        Semester GetSemesterById(string? id);
    }
}
