using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class SignInViewModel
    {
        [ForumRequired]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        [ForumRequired]
        public string Password { get; set; }

        public bool UserNotFound { get; set; } = false;

        public bool WrongPassword { get; set; } = false;
    }
}
