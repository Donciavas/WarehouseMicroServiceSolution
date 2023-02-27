using System.ComponentModel.DataAnnotations;

namespace PersonRegistrationASPNet.BusinessLogic.Attributes
{
    public class NotLessThanNumberAttribute : ValidationAttribute
    {
        private readonly double _inputLessThan;
        public NotLessThanNumberAttribute(double inputLessThan)
        {
            _inputLessThan = inputLessThan;
        }
        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            if (value is double input)
            {
                if (input < _inputLessThan)
                    return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
        private string GetErrorMessage()
        {
            return $"Input can't be less than {_inputLessThan}";
        }

    }

}
