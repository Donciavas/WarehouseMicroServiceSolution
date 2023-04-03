using DataAccess.DTOs;
using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        protected readonly IRepository<Order, string> _orderRepository;
        public OrderService(IRepository<Order, string> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Order>> GetAll() =>
            await _orderRepository.GetAll();
        public async Task<Order> Get(string id) =>
            await _orderRepository.Get(id);
        public async Task<ResponseDto> Add(Order order) =>
            await _orderRepository.Add(order);
        public async Task<ResponseDto> Update(Order order) =>
            await _orderRepository.Update(order);
        public async Task<ResponseDto> Remove(string id) =>
            await _orderRepository.Remove(id);
    }
}
