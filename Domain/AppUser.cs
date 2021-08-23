using System;
using System.Collections.Generic;

namespace Domain
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }

        public string UserName { get; set; }

        // public Guid AssignedTaskId { get; set; }
        // public Task AssignedTask { get; set; }

        // public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}