using System.ComponentModel.DataAnnotations;

namespace Challengify.Api.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public int RequiredScore { get; set; } // Score needed to unlock
        public int RequiredStreak { get; set; } // Streak needed to unlock
    }
}
