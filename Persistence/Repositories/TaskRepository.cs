using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Persistence.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(PlutoContext context) : base(context)
        {
        }

        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignee(AppUser user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByCreator(AppUser user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
