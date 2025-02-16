using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Challengify.Api.Models
{
    public class User : IdentityUser
    {
        public int Score { get; set; } = 0;
        public int Streak { get; set; } = 0;
        public string AvatarUrl { get; set; } = string.Empty;
        public int Level { get; set; } = 1;
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public List<Achievement> Badges { get; set; } = new List<Achievement>();
    }
}
