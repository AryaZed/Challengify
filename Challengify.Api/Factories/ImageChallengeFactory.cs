using Challengify.Api.Models;

namespace Challengify.Api.Factories
{
    public class ImageChallengeFactory : IChallengeFactory
    {
        public Challenge CreateChallenge()
        {
            return new Challenge
            {
                Title = "Meme Creator",
                Description = "Create a funny meme and upload it!",
                Points = 20,
                Date = DateTime.UtcNow
            };
        }
    }
}
