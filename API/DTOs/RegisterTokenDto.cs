using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterTokenDto
    {
        [Required(ErrorMessage = "'Token' must not be empty.")]
        public string Token { get; set; }
    }
}
