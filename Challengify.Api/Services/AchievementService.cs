using Challengify.Api.Data;
using Challengify.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Challengify.Api.Services
{
    public class AchievementService
    {
        private readonly ApplicationDbContext _context;

        public AchievementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CheckAndAwardAchievementsAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            var achievements = await _context.Achievements.ToListAsync();
            var earnedAchievements = await _context.UserAchievements
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AchievementId)
                .ToListAsync();

            foreach (var achievement in achievements)
            {
                if (!earnedAchievements.Contains(achievement.Id) &&
                    user.Score >= achievement.RequiredScore &&
                    (achievement.RequiredStreak == 0 || user.Streak >= achievement.RequiredStreak))
                {
                    _context.UserAchievements.Add(new UserAchievement
                    {
                        UserId = userId,
                        AchievementId = achievement.Id,
                        EarnedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Achievement>> GetUserAchievementsAsync(string userId)
        {
            return await _context.UserAchievements
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.Achievement)
                .ToListAsync();
        }

        internal async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            return await _context.Achievements
               .ToListAsync();
        }
    }
}
