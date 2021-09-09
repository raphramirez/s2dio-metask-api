using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "'Username' must not be empty.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "'Password' must not be empty.")]
        [MinLength(8, ErrorMessage = "Password must contain at least eight characters long")]
      
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Password must contain uppercase and lowercase letters and a number.")]
        public string Password { get; set; }
    }
}