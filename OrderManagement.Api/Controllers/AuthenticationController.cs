using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Email == "admin@gmail.com" && request.Password == "admin123")
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "3f3179b2-a33b-4b99-ac28-35b7002adf75"),
                new Claim(ClaimTypes.Email, request.Email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisaverystrongsecretkey123456!"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "OrderManagementApi",
                    audience: "OrderManagementClient",
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return Unauthorized();
            }
            
        }
    }
}
