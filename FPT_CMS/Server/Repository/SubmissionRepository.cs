using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class SubmissionRepository : ISubmissionRepo
    {
        public Submission AddSubmission(Submission submission) => SubmissionManagement.Instance.AddSubmission(submission);
        public Submission GetExactSubmission(int topicId, int studentId) => SubmissionManagement.Instance.GetExactSubmission(topicId, studentId);
        public Submission DelteSubmission(int submissionId) => SubmissionManagement.Instance.DeleteSubmission(submissionId);
        public List<Submission> GetSubmissionsOfTopic(int topicId) => SubmissionManagement.Instance.GetSubmissionsOfTopic(topicId);

        public Submission GetSubmissionById(int submissionId) => SubmissionManagement.Instance.GetSubmissionById(submissionId);
    }
}
