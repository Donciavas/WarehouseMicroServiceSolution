using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PersonRegistrationASPNet.BusinessLogic.Attributes
{
    public class CheckForWhiteSpacesAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
           object? value, ValidationContext validationContext)
        {
            var inValid = new Regex("[\\s]");
            if (value is string input)
            {
                if (inValid.IsMatch(input))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
        private static string GetErrorMessage()
        {
            return $"Input can't have white space";

        }
    }
}
