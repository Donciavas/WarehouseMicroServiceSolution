using DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using PersonRegistrationASPNet.BusinessLogic.Attributes;

namespace BusinessLogic.DTOs
{
    public class CustomerPutDto
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Character length between 3 and 20 for Customer Name! ")]
        [CheckForWhiteSpaces]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Customer name. Use letters only please")]
        public string? CustomerName { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Write Mobile number in digits only. ")]
        [CheckForWhiteSpaces]
        public string? MobileNumber { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|email)$", ErrorMessage = "Invalid email address pattern.")]
        public string? Email { get; set; }

        public static implicit operator Customer(CustomerPutDto customerPutDto)
        {
            return new Customer
            {
                CustomerId = customerPutDto.CustomerId,
                CustomerName = customerPutDto.CustomerName,
                MobileNumber = customerPutDto.MobileNumber,
                Email = customerPutDto.Email
            };
        }
    }
}
