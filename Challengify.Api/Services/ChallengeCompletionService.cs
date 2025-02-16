using Challengify.Api.Data;
using Challengify.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Challengify.Api.Services
{
    public class ChallengeCompletionService
    {
        private readonly ApplicationDbContext _context;

        public ChallengeCompletionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CompleteChallengeAsync(string userId, int challengeId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            bool alreadyCompleted = await _context.UserChallenges
                .AnyAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);

            if (alreadyCompleted)
                return false;

            var challenge = await _context.Challenges.FindAsync(challengeId);
            if (challenge == null) return false;

            user.Score += challenge.Points;
            int bonusPoints = 0;

            // Check streak logic
            var lastChallenge = await _context.UserChallenges
                .Where(uc => uc.UserId == userId)
                .OrderByDescending(uc => uc.CompletedAt)
                .FirstOrDefaultAsync();

            if (lastChallenge != null && lastChallenge.CompletedAt.Date == DateTime.UtcNow.Date.AddDays(-1))
            {
                user.Streak += 1;
            }
            else
            {
                user.Streak = 1;
            }

            // Award streak bonuses
            if (user.Streak == 3)
                bonusPoints = 5;
            else if (user.Streak == 7)
                bonusPoints = 15;
            else if (user.Streak == 30)
                bonusPoints = 50;

            user.Score += bonusPoints;

            _context.UserChallenges.Add(new UserChallenge
            {
                UserId = userId,
                ChallengeId = challengeId,
                CompletedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            // Award achievements (if any)
            var achievementService = new AchievementService(_context);
            await achievementService.CheckAndAwardAchievementsAsync(userId);

            return true;
        }
    }
}
