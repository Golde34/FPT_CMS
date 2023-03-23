using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ITopicRepo
    {
        IEnumerable<Topic> GetTopicsByCourseId(string courseId);
        Topic GetTopicById(int topicId);
        Topic AddTopic(Topic topic);
        Topic UpdateTopic(Topic topic);
        Topic DeleteTopic(int topicId);
    }
}
