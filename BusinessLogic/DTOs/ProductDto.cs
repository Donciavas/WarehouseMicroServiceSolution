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
        [MinInputLength(2)]
        [Required]
        [CheckForWhiteSpaces]
        [Column("product_name")]
        public string? ProductName { get; set; }
        [Required]
        [CheckForWhiteSpaces]
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
