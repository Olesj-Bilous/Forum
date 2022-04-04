using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class RegisterViewModel
    {
        [Display(Prompt = "Username")]
        [ForumRequired]
        public string Username { get; set; }

        [Display(Prompt = "Password")]
        [DataType(DataType.Password)]
        [ForumRequired]
        public string Password { get; set; }

        [Display(Name = "Repeat password", Prompt = "Repeat password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "These passwords do not match.")]
        [ForumRequired]
        public string PasswordCheck { get; set; }

        [Display(Prompt = "Email address")]
        [DataType(DataType.EmailAddress)]
        [ForumRequired]
        public string Email { get; set; }

        public bool NameNotUnique { get; set; } = false;

        public bool EmailNotUnique { get; set; } = false;
    }
}
