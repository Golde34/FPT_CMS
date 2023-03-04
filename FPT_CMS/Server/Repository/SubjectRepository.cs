using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class SubjectRepository : ISubjectRepo
    {
        Subject ISubjectRepo.AddSubject(Subject Subject) => SubjectManagement.Instance.AddSubject(Subject);
        Subject ISubjectRepo.UpdateSubject(Subject Subject) => SubjectManagement.Instance.UpdateSubject(Subject);
        Subject ISubjectRepo.DeleteSubject(Subject Subject) => SubjectManagement.Instance.DeleteSubject(Subject);
        IEnumerable<Subject> ISubjectRepo.GetSubjects() => SubjectManagement.Instance.GetSubjects();
        Subject ISubjectRepo.GetSubjectById(string? id) => SubjectManagement.Instance.GetSubjectById(id);
    }
}
