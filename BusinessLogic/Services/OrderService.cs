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
       public async Task<IEnumerable<Order>> GetAll()
        => await _orderRepository.GetAll();
       public async Task<Order> Get(string id)
        => await _orderRepository.Get(id);
       public async Task<Order> Add(Order order)
        {
            await _orderRepository.Add(order);
            return order;
        }
       public async Task<Order> Update(Order order)
        {
            await _orderRepository.Update(order);
            return order;
        }
       public async Task Remove(string id)
        {
            var order = await _orderRepository.Get(id);
            if (order != null)
            {
                await _orderRepository.Remove(id);
            }
        }
    }
}
