using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserTask
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}