using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    [Table("product")]
    public class ProductDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        public string? ProductName { get; set; }
        [Column("product_code")]
        public string? ProductCode { get; set; }
        [Column("product_price")]
        public decimal ProductPrice { get; set; }
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
