using Challengify.Api.Data;
using Challengify.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Challengify.Api.Services
{
    public class StreakService
    {
        private readonly ApplicationDbContext _context;

        public StreakService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetRecentStreakBonusAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return 0;

            // Check if a streak bonus was awarded in the last 24 hours
            var lastChallenge = await _context.UserChallenges
                .Where(uc => uc.UserId == userId)
                .OrderByDescending(uc => uc.CompletedAt)
                .FirstOrDefaultAsync();

            if (lastChallenge == null) return 0;

            // Return the most recent streak bonus
            if (user.Streak == 3) return 5;
            if (user.Streak == 7) return 15;
            if (user.Streak == 30) return 50;

            return 0;
        }
    }
}
