using Microsoft.EntityFrameworkCore;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Models
{
    public class TrackerDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options)
        {
        }
    }
}