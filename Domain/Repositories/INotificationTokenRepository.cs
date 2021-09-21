using Domain.Entities;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface INotificationTokenRepository : IRepository<NotificationToken>
    {
        Task<IEnumerable<string>> GetUserTokens(AppUser assignee);
    }
}
