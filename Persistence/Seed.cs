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
            if (!userManager.Users.Any() && !context.Tasks.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Raph",
                        UserName = "raph",
                    },
                    new AppUser
                    {
                        DisplayName = "Jade",
                        UserName = "jade",
                    },
                    new AppUser
                    {
                        DisplayName = "April",
                        UserName = "april",
                    },
                    new AppUser
                    {
                        DisplayName = "Russel",
                        UserName = "russell",
                    },
                    new AppUser
                    {
                        DisplayName = "Hanz",
                        UserName = "hanz",
                    },
                    new AppUser
                    {
                        DisplayName = "Jhie",
                        UserName = "jhie",
                    },
                    new AppUser
                    {
                        DisplayName = "Elbert",
                        UserName = "elbert",
                    },
                    new AppUser
                    {
                        DisplayName = "Genesis",
                        UserName = "genesis",
                    },
                    new AppUser
                    {
                        DisplayName = "Jhude",
                        UserName = "jhude",
                    },
                    new AppUser
                    {
                        DisplayName = "Mon",
                        UserName = "mon",
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
                        Name = "Cleaner",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(1),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        Assignee = users[3],
                        Creator = users[0]
                    },
                    new Domain.Task
                    {
                        Name = "Cleaner",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(2),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        Assignee = users[4],
                        Creator = users[0]
                    },
                    new Domain.Task
                    {
                        Name = "Cleaner",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(3),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        Assignee = users[5],
                        Creator = users[0]
                    },
                    new Domain.Task
                    {
                        Name = "Cleaner",
                        Description = "test description",
                        Date = DateTime.Now.AddDays(4),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        Assignee = users[6],
                        Creator = users[0]
                    },
                };

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}