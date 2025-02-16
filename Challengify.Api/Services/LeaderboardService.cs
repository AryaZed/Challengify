using Microsoft.EntityFrameworkCore;
using Challengify.Api.Data;
using Challengify.Api.Models;

namespace Challengify.Api.Services
{
    public class LeaderboardService
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaderboardEntry>> GetTopPlayersAsync(int topN = 10)
        {
            // Fetch users from the database first
            var users = await _context.Users
                .OrderByDescending(u => u.Score)
                .Take(topN)
                .ToListAsync(); 

            // Assign ranking in memory
            var leaderboard = users.Select((user, index) => new LeaderboardEntry
            {
                UserId = user.Id,
                Username = user.UserName ?? "Unknown",
                AvatarUrl = user.AvatarUrl,
                Score = user.Score,
                Rank = index + 1 
            }).ToList();

            return leaderboard;
        }
    }
}
