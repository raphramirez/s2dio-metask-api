﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public class CreateTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrganizationId { get; set; }
        public DateTime Date { get; set; }
        public List<AssigneeDto> Assignees { get; set; } = new List<AssigneeDto>();
    }

    public class AssigneeDto
    {
        public string Id { get; set; }
    }
}
