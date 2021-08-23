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

    // protected override void OnModelCreating (ModelBuilder modelBuilder) 
    // {
    //   // var task = modelBuilder.Entity<Task>();

    //   // task.HasOne(t => t.Creator).WithMany(c => c.Tasks).HasForeignKey(t => t.CreatorId);

    //   // var user = modelBuilder.Entity<AppUser>();

    //   // user.HasOne(u => u.AssignedTask).WithOne(a => a.Assignee).HasForeignKey<AppUser>(x => x.AssignedTaskId);
    // }
  }
}