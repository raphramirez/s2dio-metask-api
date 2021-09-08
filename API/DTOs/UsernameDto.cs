using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UsernameDto
    {
        [Required(ErrorMessage = "'Username' must not be empty.")]
        public string Username { get; set; }
    }
}
