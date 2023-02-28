using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using PersonRegistrationASPNet.BusinessLogic.Attributes;

namespace BusinessLogic.DTOs
{
    [Index(nameof(Email), IsUnique = true)]
    [Table("customer", Schema = "dbo")]
    public class CustomerDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Character length between 3 and 20 for Customer Name! ")]
        [CheckForWhiteSpaces]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Customer name. Use letters only please")]
        [Column("customer_name")]
        public string? CustomerName { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Write Mobile number in digits only. ")]
        [CheckForWhiteSpaces]
        [Column("mobile_no")]
        public string? MobileNumber { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|email)$", ErrorMessage = "Invalid email address pattern.")]
        [Column("email")]
        public string? Email { get; set; }

        public static implicit operator Customer(CustomerDto customer)
        {
            return new Customer
            {
                CustomerName = customer.CustomerName,
                MobileNumber = customer.MobileNumber,
                Email = customer.Email
            };
        }
    }
}
