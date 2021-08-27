using System;
using System.Collections.Generic;

namespace Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public ICollection<UserTask> Assignees { get; set; } = new List<UserTask>();
    }
}