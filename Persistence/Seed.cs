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
        public static async System.Threading.Tasks.Task SeedData(PlutoContext context)
        {
            // Register existing user
            var users = new List<AppUser>();
            if (!context.AppUsers.Any())
            {
                users.Add(
                    new AppUser
                    {
                        Id = "google-oauth2|101066496962928054107",
                        Name = "Ralph Ramirez",
                        Email = "ralph@s2dioapps.com",
                        Nickname = "ralph",
                        Picture = "https://lh3.googleusercontent.com/a/AATXAJyleESGVT5rpdqkdAbDMdS-_icD7LQ9wezWnAfq=s96-c"
                    });
            }

            var tasks = new List<Domain.Entities.Task>();

            if (!context.Tasks.Any())
            {
                // Get Dummy Tasks
                tasks.Add(
                    new Domain.Entities.Task
                    {
                        Name = "Dummy Task 1",
                        Description = "Test description for a dummy task.",
                        Date = DateTime.Now.AddDays(1),
                        DateCreated = DateTime.Now,
                        IsCompleted = false,
                        OrganizationId = "org_gRkmgUcVrzZtjYdv",
                        CreatedBy = users[0],
                        UserTasks = new List<UserTask>
                        {
                        new UserTask
                        {
                            AppUser = users[0],
                        }
                        }
                    }
                );
            }

            await context.AppUsers.AddRangeAsync(users);
            await context.Tasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }
    }
}