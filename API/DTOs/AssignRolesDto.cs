using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AssignRolesDto
    {
        public string UserId { get; set; }
        public List<string> Ids { get; set; } = new List<string>();
    }
}
