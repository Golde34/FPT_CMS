using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ISubjectRepo
    {
        Subject AddSubject(Subject Subject);
        Subject UpdateSubject(Subject Subject);
        Subject DeleteSubject(Subject Subject);
        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectById(string? id);
    }
}
