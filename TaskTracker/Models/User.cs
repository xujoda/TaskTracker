using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
