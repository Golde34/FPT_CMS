using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IO;
using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubmissionController : Controller
	{
        private IStudentRepo studentRepo = new StudentRepository();
        private ISubmissionRepo submissionRepo = new SubmissionRepository();
        private readonly IWebHostEnvironment _env;

        public SubmissionController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("{topicId}")]
        public IActionResult GetSubmissions(int topicId)
        {
            List<Submission> submissions = submissionRepo.GetSubmissionsOfTopic(topicId);

            return Ok(submissions);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("{topicId}")]
        public IActionResult GetSubmission(int topicId)
        {
            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            string[] tokens = token.Split(" ");


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
            var id = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            var studentId = studentRepo.GetStudentByAccountId(id).Id;

            Submission submission = submissionRepo.GetExactSubmission(topicId, studentId);
            if(submission == null)
            {
                return NotFound();
            }

            return Ok(submission);
        }

        [HttpPost]
        public IActionResult AddSubmission([FromForm] IFormFile file, [FromForm] int topicId, [FromForm] string name)
        {
            var webRootPath = _env.WebRootPath;
            if (!Directory.Exists(webRootPath + "\\Submission\\"))
            {
                Directory.CreateDirectory(webRootPath + "\\Submission\\");
            }

            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            string[] tokens = token.Split(" ");


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
            var id = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            Submission sub = submissionRepo.GetExactSubmission(topicId, studentRepo.GetStudentByAccountId(id).Id);
            if(sub != null)
            {
                if (System.IO.File.Exists(sub.URL))
                {
                    // Delete the file
                    System.IO.File.Delete(sub.URL);
                }
                submissionRepo.DelteSubmission(sub.Id);
            }

            string path = webRootPath + "\\Submission\\" + id + "_" + name + ".rar";
            using (FileStream fileStream = System.IO.File.Create(path))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            Submission submission = new Submission();
            submission.Id = 0;
            submission.SubmitDate = DateTime.Now;
            submission.URL = path;
            submission.StudentId = studentRepo.GetStudentByAccountId(id).Id;
            submission.TopicId = topicId;

            submissionRepo.AddSubmission(submission);

            return Ok();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public IActionResult UpdateMark(int submissionId, float mark)
        {
            Submission submission = submissionRepo.GetSubmissionById(submissionId);

            return Ok();
        }
    }
}
