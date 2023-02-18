using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IRepository<Order, string> _orderRepository;

        public OrderController(IRepository<Order, string> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAll();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(string? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.Get(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderRepository.Add(order);
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            await _orderRepository.Update(order);
            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(string? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.Get(orderId);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.Remove(orderId);
            return RedirectToAction(nameof(GetOrders));
        }
    }
}
