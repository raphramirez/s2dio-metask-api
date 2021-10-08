using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
        public string OrganizationId { get; set; }
        public string CreatorId { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
    }
}