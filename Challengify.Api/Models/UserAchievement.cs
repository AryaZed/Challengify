using System.ComponentModel.DataAnnotations;

namespace Challengify.Api.Models
{
    public class UserAchievement
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int AchievementId { get; set; }
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Achievement Achievement { get; set; } = null!;
    }
}
