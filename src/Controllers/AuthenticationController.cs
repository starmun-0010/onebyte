using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneByte.Contracts.RequestModels;

namespace OneByte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRequestModel userRegisterDto)
        {
            var user = new IdentityUser { UserName = userRegisterDto.Username };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserRequestModel userRegisterDto)
        {
            var user = await _userManager.FindByNameAsync(userRegisterDto.Username);
            var result = await _userManager.CheckPasswordAsync(user, userRegisterDto.Password);
            if (result)
            {
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Audience"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserName) },
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return Ok();
        }
    }
}