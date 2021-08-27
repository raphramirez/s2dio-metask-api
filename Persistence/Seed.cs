using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!context.Tasks.Any() && !userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        UserName = "raph"
                    },
                    new AppUser
                    {
                        UserName = "russel"
                    },
                    new AppUser
                    {
                        UserName = "hanz"
                    },
                    new AppUser
                    {
                        UserName = "genesis"
                    },
                    new AppUser
                    {
                        UserName = "jhie"
                    },
                    new AppUser
                    {
                        UserName = "elbert"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var tasks = new List<Domain.Task>
                {
                    new Domain.Task
                    {
                        Name = "Cleaning",
                        Description = "This is a test description.",
                        Date = DateTime.Now.AddDays(1),
                        CreatedBy = users[0],
                        DateCreated = DateTime.Now,
                        Assignee = users[3],
                        IsCompleted = false,
                    },
                    new Domain.Task
                    {
                        Name = "Dishwashing",
                        Description = "This is a test description.",
                        Date = DateTime.Now.AddDays(2),
                        CreatedBy = users[0],
                        DateCreated = DateTime.Now,
                        Assignee = users[3],
                        IsCompleted = false,
                    },
                    new Domain.Task
                    {
                        Name = "Kitchen",
                        Description = "This is a test description.",
                        Date = DateTime.Now.AddDays(2),
                        CreatedBy = users[0],
                        DateCreated = DateTime.Now,
                        Assignee = users[3],
                        IsCompleted = false,
                    },
                };

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}