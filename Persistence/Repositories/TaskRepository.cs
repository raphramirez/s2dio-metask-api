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

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignee(AppUser assignee)
        {
            return await _context.Tasks
                .Include(task => task.Assignee)
                .Where(task => task.Assignee.UserName == assignee.UserName)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByCreator(AppUser creator)
        {
            return await _context.Tasks
                .Include(task => task.CreatedBy)
                .Where(task => task.CreatedBy.UserName == creator.UserName)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByDate(DateTime date, params Expression<Func<Task, object>>[] includes)
        {
            var query = _context.Tasks
                .OrderBy(t => t.Date)
                .Where(t => t.Date >= date && t.Date < date.AddDays(1));

            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public async System.Threading.Tasks.Task<int> ToggleComplete(Task task)
        {
            task.IsCompleted = !task.IsCompleted;

            return await _context.SaveChangesAsync();
        }
    }
}
