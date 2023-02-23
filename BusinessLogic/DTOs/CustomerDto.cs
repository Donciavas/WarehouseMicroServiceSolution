using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    [Table("customer", Schema = "dbo")]
    public class CustomerDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("customer_name")]
        public string? CustomerName { get; set; }
        [Column("mobile_no")]
        public string? MobileNumber { get; set; }
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
