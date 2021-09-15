using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class PlutoContext : IdentityDbContext<AppUser>
    {
        public PlutoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<NotificationToken> NotificationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Task>()
              .HasOne<AppUser>(u => u.Assignee)
              .WithMany(t => t.Tasks)
              .HasForeignKey(s => s.AssigneeId);
        }
    }
}