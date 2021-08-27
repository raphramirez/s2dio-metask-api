using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}