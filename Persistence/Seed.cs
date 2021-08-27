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
                        Name = "Task 1",
                        Description = "test description",
                        CreatedBy = users[0],
                        Assignees = new List<UserTask>
                        {
                            new UserTask
                            {
                                AppUser = users[1],
                                Date = DateTime.Now.AddDays(2),
                                DateCreated = DateTime.Now,
                            },
                            new UserTask
                            {
                                AppUser = users[2],
                                Date = DateTime.Now.AddDays(3),
                                DateCreated = DateTime.Now,
                            },
                            new UserTask
                            {
                                AppUser = users[3],
                                Date = DateTime.Now.AddDays(4),
                                DateCreated = DateTime.Now,
                            },
                        }
                    },
                    new Domain.Task
                    {
                        Name = "Task 2",
                        Description = "test description",
                        CreatedBy = users[0],
                        Assignees = new List<UserTask>
                        {
                             new UserTask
                            {
                                AppUser = users[3],
                                Date = DateTime.Now.AddDays(4),
                                DateCreated = DateTime.Now,
                            },
                        }
                    },
                    new Domain.Task
                    {
                        Name = "Task 3",
                        Description = "test description",
                        CreatedBy = users[0],
                    },
                    new Domain.Task
                    {
                        Name = "Task 4",
                        Description = "test description",
                        CreatedBy = users[0],
                    },
                };

        await context.Tasks.AddRangeAsync(tasks);
        await context.SaveChangesAsync();
      }
    }
  }
}