using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAll();
            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByIdOrder(string orderId)
        {
            if (orderId is null) return BadRequest("Order ID cannot be empty data.");
            var order = await _orderService.Get(orderId);
            if (order is null) return NotFound($"No order found by this No. {orderId} ID.");
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            if (orderDto is null) return BadRequest("Cannot create order without any data.");
            var response = await _orderService.Add(orderDto);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);
            return StatusCode(201, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderPutDto orderPutDto)
        {
            if (orderPutDto is null) return BadRequest("Order ID cannot be empty data.");
            var response = await _orderService.Update(orderPutDto);
            if (!response.IsSuccess) return NotFound(response.Message);
            return Ok(response);
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            if (orderId is null) return BadRequest("Order ID cannot be empty data.");
            var response = await _orderService.Remove(orderId);
            if (!response.IsSuccess) return NotFound(response.Message);
            return RedirectToAction(nameof(GetOrders));
        }
    }
}
