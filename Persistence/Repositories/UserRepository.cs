using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly PlutoContext _context;

        public UserRepository(PlutoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> FindByAuth0Id(string id)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
