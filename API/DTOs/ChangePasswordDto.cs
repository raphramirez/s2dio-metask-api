using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "'Old Password' must not be empty.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "'New Password' must not be empty.")]
        [MinLength(8, ErrorMessage = "Password must contain at least eight characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain uppercase and lowercase letters and a number.")]
        public string NewPassword { get; set; }
    }
}
