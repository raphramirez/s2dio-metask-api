﻿using Domain.Entities;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByUsername(string username);
        Task<int> AddToken(AppUser user, string token);
    }
}