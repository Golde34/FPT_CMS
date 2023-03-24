using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Entity.Enum;
using Server.Repository.@interface;
using Server.Repository;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using Server.Entity;
using System.Reflection;
using System.Net;

namespace Server.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : Controller
    {
        private IStudentRepo _studentRepo = new StudentRepository();

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult Detail()
        {
            // Decode the token and get the role of account
            StringValues values;
            Request.Headers.TryGetValue("Authorization", out values);
            var token = values.ToString();
            string[] tokens = token.Split(" ");


            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokens[1]);
            var accountId = jwtSecurityToken.Claims.First(claim => claim.Type == "Id").Value;

            Student student = _studentRepo.GetStudentByAccountId(accountId);
            if (student == null) return BadRequest("No data exists");

            return Ok(new
            {
                Id = student.Id,
                StudentRollNumber = student.StudentRollNumber,
                StudentName = student.StudentName,
                Majors = Enum.GetName(typeof(StudentMajors), student.Majors),
                CurriculumId = student.CurriculumId,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                AccountId = student.AccountId
            });
        }
    }
}
