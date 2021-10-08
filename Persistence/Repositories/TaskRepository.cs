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

        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByDate(DateTime date, params Expression<Func<Task, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<int> ToggleComplete(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
