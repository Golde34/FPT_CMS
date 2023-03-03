using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class CurrDetailRepository : ICurrDetailRepo
    {
        CurriculumDetail ICurrDetailRepo.AddCurriculumDetail(CurriculumDetail CurriculumDetail) => CurrDetailManagement.Instance.AddCurriculumDetail(CurriculumDetail);
        CurriculumDetail ICurrDetailRepo.UpdateCurriculumDetail(CurriculumDetail CurriculumDetail) => CurrDetailManagement.Instance.UpdateCurriculumDetail(CurriculumDetail);
        CurriculumDetail ICurrDetailRepo.DeleteCurriculumDetail(CurriculumDetail CurriculumDetail) => CurrDetailManagement.Instance.DeleteCurriculumDetail(CurriculumDetail);
        IEnumerable<CurriculumDetail> ICurrDetailRepo.GetCurriculumDetails() => CurrDetailManagement.Instance.GetCurriculumDetails();
        CurriculumDetail ICurrDetailRepo.GetCurriculumDetailById(int? id) => CurrDetailManagement.Instance.GetCurriculumDetailById(id);
    }
}
