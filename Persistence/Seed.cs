using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedData (DataContext context)
        {
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