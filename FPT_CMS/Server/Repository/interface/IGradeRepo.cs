using Server.Entity;

namespace Server.Repository.@interface
{
    public interface IGradeRepo
    {
        Grade AddGrade(Grade Grade);
        Grade UpdateGrade(Grade Grade);
        Grade DeleteGrade(Grade Grade);
        IEnumerable<Grade> GetGrades();
        Grade GetGradeById(int? id);
    }
}
