using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITaskRepository : IRepository<Entities.Task>
    {
        Task<IEnumerable<Entities.Task>> GetByCreator(Entities.AppUser user);
        Task<IEnumerable<Entities.Task>> GetByAssignee(Entities.AppUser user);
        Task<IEnumerable<Entities.Task>> GetByDate(DateTime date);
    }
}
