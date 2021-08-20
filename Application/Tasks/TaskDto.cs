using System;
using Domain;

namespace Application.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }

        // Assignee
        public string Assignee { get; set; }

        // Created By
        public string CreatedBy { get; set; }
    }
}