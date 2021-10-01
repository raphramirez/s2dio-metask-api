using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public ICollection<NotificationToken> Tokens { get; set; } = new List<NotificationToken>();
    }
}