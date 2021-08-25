using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  public class DataContext : IdentityDbContext<AppUser>
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserTask> UserTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      // set the primary key
      builder.Entity<UserTask>(x => x.HasKey(ut => new { ut.AppUserId, ut.TaskId }));

      builder.Entity<UserTask>()
        .HasOne(u => u.AppUser)
        .WithMany(t => t.Tasks)
        .HasForeignKey(ut => ut.AppUserId);

      builder.Entity<UserTask>()
        .HasOne(u => u.Task)
        .WithMany(t => t.Assignees)
        .HasForeignKey(ut => ut.TaskId);
    }
  }
}