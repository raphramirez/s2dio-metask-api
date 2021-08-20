using System;

namespace Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }

        // Assignee
        public AppUser Assignee { get; set; }

        // Created By
        public AppUser Creator { get; set; }
    }
}