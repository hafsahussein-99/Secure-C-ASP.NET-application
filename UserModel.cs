using System.ComponentModel.DataAnnotations;

namespace SecureWebApp.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is mandatory.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Only letters, numbers, and underscores are allowed.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is mandatory.")]
        [EmailAddress(ErrorMessage = "Invalid email formatting structure.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
