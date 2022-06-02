using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneByte.Models.DTO;

namespace OneByte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = new IdentityUser { UserName = userRegisterDto.Username };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            return Ok(result);
        }
    }
}