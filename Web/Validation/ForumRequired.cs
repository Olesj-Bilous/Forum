using System.ComponentModel.DataAnnotations;

namespace Web.Validation
{
    public class ForumRequired : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult($"{validationContext.DisplayName} is required.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
