using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Services
{
    public interface IOrderService : IService<Order, string>
    {
        Task<Order> AddDto(OrderDto orderDto);
    }
}
