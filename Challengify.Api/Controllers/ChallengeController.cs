using Microsoft.AspNetCore.Mvc;
using Challengify.Api.Services;
using Challengify.Api.Models;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/challenge")]
    public class ChallengeController : ControllerBase
    {
        private readonly ChallengeService _challengeService;

        public ChallengeController()
        {
            _challengeService = new ChallengeService();
        }

        [HttpGet("daily")]
        public ActionResult<Challenge> GetDailyChallenge()
        {
            var challenge = _challengeService.GetDailyChallenge();
            return Ok(challenge);
        }
    }
}
