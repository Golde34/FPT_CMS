using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class TopicRepository : ITopicRepo
    {
        public IEnumerable<Topic> GetTopicsByCourseId(string courseId) => TopicManagement.Instance.GetTopicsByCourseId(courseId);
        public Topic GetTopicById(int topicId) => TopicManagement.Instance.GetTopicById(topicId);
        public Topic AddTopic(Topic topic) => TopicManagement.Instance.AddTopic(topic);
        public Topic DeleteTopic(int topicId) => TopicManagement.Instance.DeleteTopic(topicId);
    }
}
