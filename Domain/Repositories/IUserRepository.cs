using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<Entities.AppUser>
    {
        System.Threading.Tasks.Task<AppUser> FindByAuth0Id(string id);
    }
}
