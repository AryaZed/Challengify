using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Challengify.Api.Services;
using System.Security.Claims;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/achievements")]
    public class AchievementController : ControllerBase
    {
        private readonly AchievementService _achievementService;

        public AchievementController(AchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetUserAchievements()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var achievements = await _achievementService.GetUserAchievementsAsync(userId);
            return Ok(achievements);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAchievements()
        {
            var achievements = await _achievementService.GetAllAchievementsAsync();
            return Ok(achievements);
        }
    }
}
