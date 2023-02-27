using System.ComponentModel.DataAnnotations;

namespace PersonRegistrationASPNet.BusinessLogic.Attributes
{
    public class MinInputLengthAttribute : ValidationAttribute
    {
        private readonly int _minInputLength;
        public MinInputLengthAttribute(int minInputLength)
        {
            _minInputLength = minInputLength;
        }
        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                if (input.Length < _minInputLength)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
        private string GetErrorMessage()
        {
            return $"Input length must be more than {_minInputLength} symbols !!!";

        }
    }
}
