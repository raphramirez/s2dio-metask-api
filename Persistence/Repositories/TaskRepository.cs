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
            //task.Name = edittedTask.Name;
            //task.Description = edittedTask.Description;
            //task.Date = edittedTask.Date;

            //// update assignees
            //if (task.UserTasks != edittedTask.UserTasks)
            //{
            //    var assigneeIds = new List<string>();
            //    foreach (var userTask in edittedTask.UserTasks)
            //    {
            //        assigneeIds.Add(userTask.AppUser.Id);
            //    }

            //    task.UserTasks = await GetAssignees(assigneeIds);
            //}

            return await Context.SaveChangesAsync();
        }

        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByDate(DateTime date, params Expression<Func<Task, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<int> ToggleComplete(Task task)
        {
            throw new NotImplementedException();
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
    }
}
