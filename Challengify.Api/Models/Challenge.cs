using System.ComponentModel.DataAnnotations;

namespace Challengify.Api.Models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
