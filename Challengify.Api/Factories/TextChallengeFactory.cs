using Challengify.Api.Models;

namespace Challengify.Api.Factories
{
    public class TextChallengeFactory : IChallengeFactory
    {
        public Challenge CreateChallenge()
        {
            return new Challenge
            {
                Title = "Funny Story Time",
                Description = "Write a short funny story in 100 words!",
                Points = 10,
                Date = DateTime.UtcNow
            };
        }
    }
}
