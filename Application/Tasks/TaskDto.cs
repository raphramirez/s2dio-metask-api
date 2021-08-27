using System;
using System.Collections.Generic;
using Application.Profiles;
using Domain;

namespace Application.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public Profiles.Profile CreatedBy { get; set; }
        public ICollection<AssignedTask> Assignees { get; set; } = new List<AssignedTask>();
    }
}
