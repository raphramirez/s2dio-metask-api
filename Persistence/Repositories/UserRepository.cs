using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using System;
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

        public async Task<AppUser> GetByUsername(string username)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<int> AddToken(AppUser user, string token)
        {
            var appUser = Get(Guid.Parse(user.Id)).Result;

            appUser.Tokens.Add(new NotificationToken { Value = token });

            return await _context.SaveChangesAsync();
        }
    }
}
