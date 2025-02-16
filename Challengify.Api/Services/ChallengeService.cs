using Challengify.Api.Factories;
using Challengify.Api.Models;

namespace Challengify.Api.Services
{
    public class ChallengeService
    {
        private readonly List<IChallengeFactory> _factories;

        public ChallengeService()
        {
            _factories = new List<IChallengeFactory>
            {
                new TextChallengeFactory(),
                new MathChallengeFactory(),
                new ImageChallengeFactory()
            };
        }

        public Challenge GetDailyChallenge()
        {
            var random = new Random();
            int index = random.Next(_factories.Count);
            return _factories[index].CreateChallenge();
        }
    }
}
