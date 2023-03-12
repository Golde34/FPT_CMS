using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ISubmissionRepo
    {
        Submission AddSubmission(Submission submission);
    }
}
