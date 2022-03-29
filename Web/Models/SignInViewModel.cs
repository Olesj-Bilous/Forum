using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SignInViewModel
    {
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
