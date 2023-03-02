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
        public async Task<IActionResult> GetByIdOrder(string orderId)
        {
            if (orderId is null) return BadRequest();
            var order = await _orderService.Get(orderId);
            if (order is null) return NotFound();
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            if (orderDto is null) return BadRequest();
            var result = await _orderService.Add(orderDto);
            if (result is null) return UnprocessableEntity();
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderPutDto orderPutDto)
        {
            if (orderPutDto is null) return BadRequest();
            var result = await _orderService.Update(orderPutDto);
            if (!result) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            if (orderId is null) return BadRequest();
            var result = await _orderService.Remove(orderId);
            if (!result) return NotFound();
            return RedirectToAction(nameof(GetOrders));
        }
    }
}
