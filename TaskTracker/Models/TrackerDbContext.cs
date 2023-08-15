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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}