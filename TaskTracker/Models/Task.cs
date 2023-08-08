using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models
{
    public class Task
    {
        [Required]
        public int TaskId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }
        
        public DateTime Created { get; } = DateTime.Now;
        public DateTime Updated { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Range(1, 10)]
        public int Priority { get; set; }

        [Range(1,5)]
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
