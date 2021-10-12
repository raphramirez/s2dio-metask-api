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
        IEnumerable<Entities.Task> FilterByDate(DateTime date, IEnumerable<Entities.Task> tasks, params Expression<Func<Entities.Task, object>>[] includes);
        IEnumerable<Entities.Task> FilterByAssignee(Entities.AppUser assignee, IEnumerable<Entities.Task> tasks, params Expression<Func<Entities.Task, object>>[] includes);
        Task<int> ToggleComplete(Entities.Task task);
        Task<int> Edit();
        Task<int> AddAssignee(Entities.Task task, Entities.AppUser user);
        Task<int> RemoveAssignee(Entities.Task task, Entities.AppUser user);
    }
}