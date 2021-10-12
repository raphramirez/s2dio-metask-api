using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        private readonly PlutoContext _context;

        public TaskRepository(PlutoContext context) : base(context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<int> Edit()
        {
            return await Context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<int> AddAssignee(Task task, AppUser user)
        {
            task.UserTasks.Add(
                new UserTask
                {
                    AppUser = user
                });

            return await Context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<int> RemoveAssignee(Task task, AppUser user)
        {
            var userToRemove = task.UserTasks.FirstOrDefault(t => t.AppUserId == user.Id);

            task.UserTasks.Remove(userToRemove);

            return await Context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<int> ToggleComplete(Task task)
        {
            task.IsCompleted = !task.IsCompleted;

            return await Context.SaveChangesAsync();
        }

        private async System.Threading.Tasks.Task<List<UserTask>> GetAssignees(List<string> assigneeIds)
        {
            if (assigneeIds.Any())
            {
                var assignees = new List<UserTask>();
                foreach (var id in assigneeIds)
                {
                    var foundUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
                    assignees.Add(new UserTask
                    {
                        AppUser = foundUser
                    });
                }
                return assignees;
            }

            return null;
        }

        public IEnumerable<Task> FilterByDate(DateTime date, IEnumerable<Task> tasks, params Expression<Func<Task, object>>[] includes)
        {
            tasks = tasks.Where(t => t.Date >= date && t.Date < date.AddDays(1));

            return tasks;
        }

        public IEnumerable<Task> FilterByAssignee(AppUser assignee, IEnumerable<Task> tasks, params Expression<Func<Task, object>>[] includes)
        {
            tasks = tasks.Where(t => t.UserTasks.Any(ut => ut.AppUserId == assignee.Id));

            return tasks;
        }
    }
}
