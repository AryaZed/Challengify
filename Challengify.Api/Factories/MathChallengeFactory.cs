using Challengify.Api.Models;

namespace Challengify.Api.Factories
{
    public class MathChallengeFactory : IChallengeFactory
    {
        public Challenge CreateChallenge()
        {
            return new Challenge
            {
                Title = "Solve a Math Puzzle",
                Description = "What is 15 + 23 × 2?",
                Points = 15,
                Date = DateTime.UtcNow
            };
        }
    }
}
