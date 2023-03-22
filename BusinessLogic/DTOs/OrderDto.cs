using DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Attributes;

namespace BusinessLogic.DTOs
{
    public class OrderDto
    {
        public string? OrderId { get; set; }
        [Required]
        [CheckForWhiteSpaces]
        [NotLessThanNumber(1)]
        public int CustomerId { get; set; }
        public DateTime OrderedOn { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
        public static implicit operator Order(OrderDto orderDto)
        {
            return new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderedOn = DateTime.UtcNow,
                OrderDetails = orderDto.OrderDetails
            };
        }
    }
}
