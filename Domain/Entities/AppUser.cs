using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Picture { get; set; }
        public ICollection<NotificationToken> Tokens { get; set; } = new List<NotificationToken>();
        public ICollection<UserTask> UserTasks { get; set; }
    }
}