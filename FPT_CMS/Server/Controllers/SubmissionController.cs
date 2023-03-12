using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Server.DAO;
using Server.DTO;
using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubmissionController : Controller
	{
        private static IWebHostEnvironment _webHostEnvironment;
        private IStudentRepo studentRepo = new StudentRepository();
        private ISubmissionRepo submissionRepo = new SubmissionRepository();

        [HttpPost]
        public IActionResult AddSubmission(UploadFileDTO fileDTO)
        {
            if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Submission\\"))
            {
                Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Submission\\");
            }

            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.Replace('"', ' ').Trim());
            var id = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            string path = _webHostEnvironment.WebRootPath + "\\Submission\\" + id + "_" + fileDTO.Name + ".rar";
            using (FileStream fileStream = System.IO.File.Create(path))
            {
                fileDTO.files.CopyTo(fileStream);
                fileStream.Flush();
            }

            Submission submission = new Submission();
            submission.Id = 0;
            submission.SubmitDate = DateTime.Now;
            submission.URL = path;
            submission.StudentId = studentRepo.GetStudentByAccountId(id).Id;
            submission.TopicId = fileDTO.topicId;

            submissionRepo.AddSubmission(submission);

            return Ok();
        }
    }
}
