using demo1.Data;
using demo1.model;
using demo1.service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        IConfiguration _configuration;

        public LoginController(ApplicationDbContext applicationDbContext,
            IConfiguration configuration)
        {
            _dbContext = applicationDbContext;

            _configuration = configuration;
        }
 



            [HttpPost]
        [Route("PostLoginDetails")]
        public async Task<IActionResult> PostLoginDetails(UserModel userData)
        {
            if (userData != null)
            {
                var resultLoginCheck = _dbContext.UserTB
                    .Where(e => e.EmailId == userData.EmailId && e.Password == userData.Password)
                    .FirstOrDefault();
                if (resultLoginCheck == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                        userData.UserMessage = "Login Success";

                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", userData.ID.ToString()),
                        new Claim("DisplayName", userData.UserName),
                        new Claim("UserName", userData.UserName),
                        new Claim("Email", userData.EmailId)
                    };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);


                    userData.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(userData);
                }
            }
            else
            {
                return BadRequest("No Data Posted");
            }
        }

    }
}
