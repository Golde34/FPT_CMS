using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class SubmissionRepository : ISubmissionRepo
    {
        public Submission AddSubmission(Submission submission) => SubmissionManagement.Instance.AddSubmission(submission);
    }
}
