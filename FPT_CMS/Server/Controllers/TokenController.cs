using Server.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Repository;
using Server.Repository.@interface;

namespace Server.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        public IConfiguration _configuration;
        private IAccountRepo _accountRepo = new AccountRepository();
        private IStudentRepo _studentRepo = new StudentRepository();
        private ITeacherRepo _teacherRepo = new TeacherRepository();

        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Post(AccountDTO accountDTO)
        {
            if (ModelState.IsValid)
            {
                var acc = _accountRepo.GetAccountByUserNameAndPassword(accountDTO);

                if (acc != null)
                {
                    var name = "";
                    if (acc.Role == Entity.Enum.Roles.Student)
                    {
                        Student student = _studentRepo.GetStudentByAccountId(acc.Id);
                        if(student == null) return BadRequest("No data exists");
                        name = student.StudentName;
                    }
                    else
                    {
                        Teacher teacher = _teacherRepo.GetTeacherByAccountId(acc.Id);
                        if (teacher == null) return BadRequest("No data exists");
                        name = teacher.Name;
                    }

                    #region Tạo token

                    try
                    {
                        //create claims details based on the user information
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim(ClaimTypes.Role, Enum.GetName(acc.Role)),
                            new Claim("Id", acc.Id),
                            new Claim("Name", name)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: signIn);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    #endregion
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
