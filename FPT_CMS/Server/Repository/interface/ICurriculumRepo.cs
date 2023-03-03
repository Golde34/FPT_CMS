using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ICurriculumRepo
    {
        Curriculum AddCurriculum(Curriculum Curriculum);
        Curriculum UpdateCurriculum(Curriculum Curriculum);
        Curriculum DeleteCurriculum(Curriculum Curriculum);
        IEnumerable<Curriculum> GetCurriculums();
        Curriculum GetCurriculumById(int? id);
    }
}
