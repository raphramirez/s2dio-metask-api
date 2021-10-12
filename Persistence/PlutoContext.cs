using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class PlutoContext : DbContext
    {
        public PlutoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<NotificationToken> NotificationTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTask>().HasKey(sc => new { sc.AppUserId, sc.TaskId });

            builder.Entity<UserTask>()
                .HasOne<AppUser>(u => u.AppUser)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(ut => ut.AppUserId);

            builder.Entity<UserTask>()
                .HasOne<Task>(t => t.Task)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(ut => ut.TaskId);
        }
    }
}