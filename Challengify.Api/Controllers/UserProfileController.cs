using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Challengify.Api.Models;
using System.Security.Claims;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            return Ok(new
            {
                user.UserName,
                user.AvatarUrl,
                user.Score,
                user.Streak,
                user.Level,
                user.JoinDate
            });
        }
    }
}
