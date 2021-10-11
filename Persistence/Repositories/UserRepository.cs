using Domain.Entities;
using Domain.Repositories;
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
    }
}
