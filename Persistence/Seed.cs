using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedData(PlutoContext context, UserManager<AppUser> userManager)
        {

            string[] tasksNames =
            {
                "Dishwashing",
                "Cleaning",
                "CR",
                "Trash",
                "Kitchen",
                "Coffee Cleaning",
                "Rice Management",
                "Stock Management",
            };

            if (!context.Tasks.Any() && !userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        UserName = "elbert"
                    },
                    new AppUser
                    {
                        UserName = "russel"
                    },
                    new AppUser
                    {
                        UserName = "jhie"
                    },
                    new AppUser
                    {
                        UserName = "genesis"
                    },
                    new AppUser
                    {
                        UserName = "raph"
                    },
                    new AppUser
                    {
                        UserName = "hanz"
                    },
                    new AppUser
                    {
                        UserName = "jude"
                    },
                    new AppUser
                    {
                        UserName = "jade"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                int daysInSept = DateTime.DaysInMonth(2021, 9);
                var tasks = new List<Domain.Task>();

                var userIndex = 0;

                // Month of Sept
                for (int day = 1; day <= daysInSept; day++)
                {
                    userIndex--;
                    for (int i = 0; i < tasksNames.Length; i++)
                    {
                        if (userIndex < 0 || userIndex > users.Count - 1) userIndex = 0;

                        tasks.Add(new Domain.Task
                        {
                            Name = tasksNames[i],
                            Description = "Daily tasks.",
                            Date = new DateTime(2021, 09, day, 8, 0, 0),
                            CreatedBy = users[7],
                            DateCreated = DateTime.Now,
                            Assignee = users[userIndex],
                            IsCompleted = false,
                        });

                        userIndex++;
                    }
                }

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}