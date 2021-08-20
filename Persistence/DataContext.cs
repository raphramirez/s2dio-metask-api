using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  public class DataContext : IdentityDbContext<AppUser>
  {
    protected DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Task>()
            .HasOne(t => t.Creator)
            .WithMany(c => c.Tasks);
    }
  }
}