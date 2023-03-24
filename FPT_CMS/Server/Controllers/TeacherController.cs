using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Server.Entity;
using Server.Repository;
using Server.Repository.@interface;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers
{
    [Authorize(Roles ="Teacher")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private ITeacherRepo _teacherRepo = new TeacherRepository();

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

            Teacher teacher = _teacherRepo.GetTeacherByAccountId(accountId);
            if (teacher == null) return BadRequest("No data exists");

            return Ok(teacher);
        }
    }
}
