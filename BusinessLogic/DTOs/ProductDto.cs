using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PersonRegistrationASPNet.BusinessLogic.Attributes;

namespace BusinessLogic.DTOs
{
    [Table("product")]
    public class ProductDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int ProductId { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Character length between 3 and 20 for Product Name! ")]
        [Required]
        [CheckForWhiteSpaces]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Product Name. Use numbers and letters only please")]
        [Column("product_name")]
        public string? ProductName { get; set; }
        [Required]
        [CheckForWhiteSpaces]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Product Code. Use numbers and letters only please")]
        [Column("product_code")]
        public string? ProductCode { get; set; }
        [Required]
        [CheckForWhiteSpaces]
        [NotLessThanNumber(1)]
        [Column("product_price")]
        public double ProductPrice { get; set; }

        public static implicit operator Product(ProductDto product)
        {
            return new Product
            {
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                ProductPrice = product.ProductPrice
            };
        }
    }
}
