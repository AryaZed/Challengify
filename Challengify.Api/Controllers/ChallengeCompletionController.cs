using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Challengify.Api.Services;
using System.Security.Claims;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/challenges")]
    public class ChallengeCompletionController : ControllerBase
    {
        private readonly ChallengeCompletionService _challengeCompletionService;

        public ChallengeCompletionController(ChallengeCompletionService challengeCompletionService)
        {
            _challengeCompletionService = challengeCompletionService;
        }

        [Authorize]
        [HttpPost("complete/{challengeId}")]
        public async Task<IActionResult> CompleteChallenge(int challengeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var success = await _challengeCompletionService.CompleteChallengeAsync(userId, challengeId);

            if (!success) return BadRequest("Challenge already completed or invalid challenge.");

            return Ok(new { message = "Challenge completed successfully!" });
        }
    }
}
