using System.ComponentModel.DataAnnotations;

namespace Challengify.Api.Models
{
    public class LeaderboardEntry
    {
        [Key] 
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Rank { get; set; }
    }
}
