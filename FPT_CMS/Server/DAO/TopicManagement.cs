using Server.Entity;

namespace Server.DAO
{
    public class TopicManagement
    {
        private static TopicManagement instance;
        private static readonly object instancelock = new object();

        public TopicManagement() { }

        public static TopicManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new TopicManagement();
                }
                return instance;
            }
        }

        public IEnumerable<Topic> GetTopicsByCourseId(string courseId)
        {
            List<Topic> topics = null;
            try
            {
                var context = new AppDBContext();
                topics = context.Topics.Where(t => t.CourseId.Equals(courseId)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return topics;
        }

        public Topic GetTopicById(int topicId)
        {
            Topic topic = null;
            try
            {
                var context = new AppDBContext();
                topic = context.Topics.FirstOrDefault(t => t.Id == topicId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return topic;
        }

        public Topic AddTopic(Topic topic)
        {
            try
            {
                Topic _topic = GetTopicById(topic.Id);
                if (_topic == null)
                {
                    var context = new AppDBContext();

                    context.Topics.Add(topic);
                    context.SaveChanges();
                    return topic;
                }
                else
                {
                    throw new Exception("The Topic has already existed");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Topic DeleteTopic(int topicId)
        {
            try
            {
                Topic _topic = GetTopicById(topicId);
                if (_topic != null)
                {
                    var context = new AppDBContext();

                    List<Submission> submissions = context.Submissions.Where(s => s.TopicId == topicId).ToList();
                    context.Submissions.RemoveRange(submissions);

                    context.Topics.Remove(_topic);
                    context.SaveChanges();
                    return _topic;
                }
                else
                {
                    throw new Exception("The Topic doesn't existed");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
