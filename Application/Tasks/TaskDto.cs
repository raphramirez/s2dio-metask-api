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
        public Profiles.Profile CreatedBy { get; set; }
        public string OrganizationId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
        public List<Profiles.Profile> Assignees { get; set; }

    }
}
