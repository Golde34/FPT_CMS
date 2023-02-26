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
                            new Claim("Name", acc.Username)
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
