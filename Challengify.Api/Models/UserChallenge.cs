using System.ComponentModel.DataAnnotations;

namespace Challengify.Api.Models
{
    public class UserChallenge
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ChallengeId { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Challenge Challenge { get; set; } = null!;
    }
}
