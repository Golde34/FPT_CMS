using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class GradeRepository : IGradeRepo
    {
        Grade IGradeRepo.AddGrade(Grade Grade) => GradeManagement.Instance.AddGrade(Grade);
        Grade IGradeRepo.UpdateGrade(Grade Grade) => GradeManagement.Instance.UpdateGrade(Grade);
        Grade IGradeRepo.DeleteGrade(Grade Grade) => GradeManagement.Instance.DeleteGrade(Grade);
        IEnumerable<Grade> IGradeRepo.GetGrades() => GradeManagement.Instance.GetGrades();
        Grade IGradeRepo.GetGradeById(int? id) => GradeManagement.Instance.GetGradeById(id);
    }
}
