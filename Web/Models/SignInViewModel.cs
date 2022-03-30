using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Username is a required field.")]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }

        public bool UserNotFound { get; set; } = false;

        public bool WrongPassword { get; set; } = false;
    }
}
