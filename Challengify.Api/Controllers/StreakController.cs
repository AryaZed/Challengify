using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Challengify.Api.Services;
using System.Security.Claims;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/streaks")]
    public class StreakController : ControllerBase
    {
        private readonly StreakService _streakService;

        public StreakController(StreakService streakService)
        {
            _streakService = streakService;
        }

        [Authorize]
        [HttpGet("bonus")]
        public async Task<IActionResult> GetStreakBonus()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var bonus = await _streakService.GetRecentStreakBonusAsync(userId);
            return Ok(new { StreakBonus = bonus });
        }
    }
}
