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
    public class NotificationTokenRepository : Repository<NotificationToken>, INotificationTokenRepository
    {
        private readonly PlutoContext _context;

        public NotificationTokenRepository(PlutoContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<string>> GetUserTokens(AppUser assignee)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<string>> GetUserTokens(AppUser assignee)
        //{
        //    return await _context.NotificationTokens
        //        .Where(token => token.AppUser.UserName == assignee.UserName)
        //        .Select(token => token.Value)
        //        .ToListAsync();
        //}
    }
}
