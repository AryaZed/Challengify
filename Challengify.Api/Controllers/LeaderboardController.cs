using Microsoft.AspNetCore.Mvc;
using Challengify.Api.Services;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly LeaderboardService _leaderboardService;

        public LeaderboardController(LeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopPlayers([FromQuery] int topN = 10)
        {
            var leaderboard = await _leaderboardService.GetTopPlayersAsync(topN);
            return Ok(leaderboard);
        }
    }
}
