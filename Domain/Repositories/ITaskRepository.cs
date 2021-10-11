using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITaskRepository : IRepository<Entities.Task>
    {
        Task<IEnumerable<Entities.Task>> GetByDate(DateTime date, params Expression<Func<Entities.Task, object>>[] includes);
        Task<int> ToggleComplete(Entities.Task task);
    }
}
