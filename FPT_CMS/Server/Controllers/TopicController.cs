using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicController : Controller
    {
        public ITopicRepo topicRepo = new TopicRepository();

        [HttpGet("{courseId}")]
        public IActionResult GetTopicsByCourseId(string courseId)
        {
            List<Topic> topics = topicRepo.GetTopicsByCourseId(courseId).ToList();
            return Ok(topics);
        }

        [HttpGet("{topicId}")]
        public IActionResult GetTopicById(int topicId)
        {
            Topic topic = topicRepo.GetTopicById(topicId);
            return Ok(topic);
        }

        [HttpPost]
        public IActionResult CreateTopic(Topic topic)
        {
            Topic result = topicRepo.AddTopic(topic);
            return Ok(result);
        }

        [HttpDelete("{topicId}")]
        public IActionResult DeleteTopic(int topicId)
        {
            Topic result = topicRepo.DeleteTopic(topicId);
            return Ok(result);
        }
    }
}
