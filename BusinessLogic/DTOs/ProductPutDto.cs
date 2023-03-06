using DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using PersonRegistrationASPNet.BusinessLogic.Attributes;

namespace BusinessLogic.DTOs
{
    public class ProductPutDto
    {
        public int ProductId { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Character length between 3 and 20 for Product Name! ")]
        [Required]
        [CheckForWhiteSpaces]
        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Product Name. Use numbers and letters only please")]
        public string? ProductName { get; set; }
        [Required]
        [CheckForWhiteSpaces]
        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Product Code. Use numbers and letters only please")]
        public string? ProductCode { get; set; }
        [Required]
        [CheckForWhiteSpaces]
        [NotLessThanNumber(1)]
        public double ProductPrice { get; set; }

        public static implicit operator Product(ProductPutDto productPutDto)
        {
            return new Product
            {
                ProductId = productPutDto.ProductId,
                ProductName = productPutDto.ProductName,
                ProductCode = productPutDto.ProductCode,
                ProductPrice = productPutDto.ProductPrice
            };
        }
    }
}
