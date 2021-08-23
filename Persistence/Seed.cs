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
        public static async System.Threading.Tasks.Task SeedData (DataContext context, UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
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
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (context.Tasks.Any()) return;

            var tasks = new List<Domain.Task>
                {
                    new Domain.Task
                    {
                        Name = "Task 1",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(1),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        CreatedBy = "Raph",
                        Assignee = "Hanz",
                    },
                    new Domain.Task
                    {
                        Name = "Task 2",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(2),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        CreatedBy = "Raph",
                        Assignee = "Russel",
                    },
                    new Domain.Task
                    {
                        Name = "Task 3",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(3),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        CreatedBy = "Raph",
                        Assignee = "Gen",
                    },
                    new Domain.Task
                    {
                        Name = "Task 4",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(4),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        CreatedBy = "Raph",
                        Assignee = "Jhie",
                    },
                };

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
        }
    }
}