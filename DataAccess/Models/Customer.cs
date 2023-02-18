using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerWebApi.Models
{
    [Table("customer", Schema = "dbo")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; } = null!;

        [Column("mobile_no")]
        public string MobileNumber { get; set; } = null!;

        [Column("email")]
        public string Email { get; set; } = null!;
    }
}
