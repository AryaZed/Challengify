using Challengify.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challengify.Api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
    {
        public required DbSet<Challenge> Challenges { get; set; }
        public required DbSet<UserChallenge> UserChallenges { get; set; }
        public required DbSet<Achievement> Achievements { get; set; }
        public required DbSet<UserAchievement> UserAchievements { get; set; }
        public required DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure relationships for UserAchievements
            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.Achievement)
                .WithMany()
                .HasForeignKey(ua => ua.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships for UserChallenges
            modelBuilder.Entity<UserChallenge>()
                .HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserChallenge>()
                .HasOne(uc => uc.Challenge)
                .WithMany()
                .HasForeignKey(uc => uc.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement { Id = 1, Name = "First Challenge", Description = "Complete your first challenge!", IconUrl = "first_challenge.png", RequiredScore = 10 },
                new Achievement { Id = 2, Name = "100 Points Club", Description = "Reach 100 total points!", IconUrl = "100_points.png", RequiredScore = 100 },
                new Achievement { Id = 3, Name = "5-Day Streak", Description = "Complete challenges for 5 consecutive days!", IconUrl = "5_streak.png", RequiredScore = 0, RequiredStreak = 5 }
            );
        }
    }
}
