using DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using PersonRegistrationASPNet.BusinessLogic.Attributes;

namespace BusinessLogic.DTOs
{
    public class OrderPutDto
    {
        public string? OrderId { get; set; }
        [Required]
        [CheckForWhiteSpaces]
        [NotLessThanNumber(1)]
        public int CustomerId { get; set; }
        public DateTime OrderedOn { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }

        public static implicit operator Order(OrderPutDto orderPutDto)
        {
            return new Order
            {
                OrderId = orderPutDto.OrderId,
                CustomerId = orderPutDto.CustomerId,
                OrderedOn = DateTime.UtcNow,
                OrderDetails = orderPutDto.OrderDetails
            };
        }
    }
}
