using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.Models;
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
        public async Task<IActionResult> GetById(string? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }
           var order = await _orderService.Get(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
       [HttpPost]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            await _orderService.AddDto(orderDto);
            return Ok();
        }
       [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            await _orderService.Update(order);
            return Ok();
        }
       [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(string? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }
           var order = await _orderService.Get(orderId);
            if (order == null)
            {
                return NotFound();
            }
           await _orderService.Remove(orderId);
            return RedirectToAction(nameof(GetOrders));
        }
    }
}
