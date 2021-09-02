using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "'Username' must not be empty.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "'Password' must not be empty.")]
        [MinLength(8, ErrorMessage = "Password must contain at least eight characters long")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must contain uppercase and lowercase letters and a number.")]
        public string Password { get; set; }
    }
}