using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class CurriculumRepository : ICurriculumRepo
    {
        Curriculum ICurriculumRepo.AddCurriculum(Curriculum Curriculum) => CurriculumManagement.Instance.AddCurriculum(Curriculum);
        Curriculum ICurriculumRepo.UpdateCurriculum(Curriculum Curriculum) => CurriculumManagement.Instance.UpdateCurriculum(Curriculum);
        Curriculum ICurriculumRepo.DeleteCurriculum(Curriculum Curriculum) => CurriculumManagement.Instance.DeleteCurriculum(Curriculum);
        IEnumerable<Curriculum> ICurriculumRepo.GetCurriculums() => CurriculumManagement.Instance.GetCurriculums();
        Curriculum ICurriculumRepo.GetCurriculumById(int? id) => CurriculumManagement.Instance.GetCurriculumById(id);
    }
}
