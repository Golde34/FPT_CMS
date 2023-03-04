using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ICurrDetailRepo
    {
        CurriculumDetail AddCurriculumDetail(CurriculumDetail CurriculumDetail);
        CurriculumDetail UpdateCurriculumDetail(CurriculumDetail CurriculumDetail);
        CurriculumDetail DeleteCurriculumDetail(CurriculumDetail CurriculumDetail);
        IEnumerable<CurriculumDetail> GetCurriculumDetails();
        CurriculumDetail GetCurriculumDetailById(int? id);
    }
}
