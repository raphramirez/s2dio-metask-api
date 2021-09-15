using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public ICollection<NotificationToken> Tokens { get; set; } = new List<NotificationToken>();
    }
}