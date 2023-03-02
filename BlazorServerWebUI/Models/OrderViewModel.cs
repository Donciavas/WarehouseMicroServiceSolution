using System.ComponentModel.DataAnnotations;

namespace BlazorServerWebUI.Models
{
    public class OrderViewModel
    {
        public string? OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderedOn { get; set; }
        public List<OrderDetailViewModel>? OrderDetails { get; set; }
    }
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity. Please enter valid integer Number! ")]
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
