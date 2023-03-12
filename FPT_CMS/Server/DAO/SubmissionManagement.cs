using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;

namespace Server.DAO
{
    public class SubmissionManagement
    {
        private static SubmissionManagement instance;
        private static readonly object instancelock = new object();

        public SubmissionManagement() { }

        public static SubmissionManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new SubmissionManagement();
                }
                return instance;
            }
        }

        public Submission GetSubmissionById(int Id)
        {
            Submission submission = null;
            try
            {
                var context = new AppDBContext();
                submission = context.Submissions.FirstOrDefault(s => s.Id == Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return submission;
        }

        public Submission AddSubmission(Submission submission)
        {
            try
            {
                Submission _submission = GetSubmissionById(submission.Id);
                if (_submission == null)
                {
                    var context = new AppDBContext();

                    context.Submissions.Add(submission);
                    context.SaveChanges();
                    return submission;
                }
                else
                {
                    throw new Exception("The Submission has already existed");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
