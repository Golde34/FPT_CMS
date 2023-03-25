using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Server.DAO;
using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicController : Controller
    {
        public ITopicRepo topicRepo = new TopicRepository();
        private IStudentRepo studentRepo = new StudentRepository();
        private ICourseRepo courseRepo = new CourseRepository();
        private IEnrollmentRepo enrollmentRepo = new EnrollmentRepository();

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

        [HttpGet("{topicId}")]
        public IActionResult IsEnrolledIn(int topicId)
        {
            Topic topic = topicRepo.GetTopicById(topicId);

            string courseId = topic.CourseId;

            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            string[] tokens = token.Split(" ");


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
            var accountId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            Student student = studentRepo.GetStudentByAccountId(accountId.ToString());
            if (student == null)
            {
                return NotFound();
            }

            Course course = courseRepo.GetCourseById(courseId);
            if (course == null)
            {
                return NotFound();
            }

            Enrollment enrollment = enrollmentRepo.GetEnrollmentsByStudentId(student.Id).FirstOrDefault(e => e.CourseId.Equals(course.CourseId));

            if (enrollment == null)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateTopic(Topic topic)
        {
            Topic result = topicRepo.AddTopic(topic);
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Teacher")]
        public IActionResult UpdateTopic(Topic topic)
        {
            Topic t = topicRepo.GetTopicById(topic.Id);

            if(t == null)
            {
                return NotFound();
            }

            topicRepo.UpdateTopic(topic);

            return Ok();
        }

        [HttpDelete("{topicId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteTopic(int topicId)
        {
            Topic result = topicRepo.DeleteTopic(topicId);
            return Ok(result);
        }
    }
}
