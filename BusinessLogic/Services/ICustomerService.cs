using DataAccess.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Services
{
    public interface ICustomerService : IService<Customer, int>
    {
        Task<IEnumerable<Customer>> GetOrderedByName();
        ResponseDto GreetingEmail(string verifyEmail);
        ResponseDto EmailCheck(string checkEmail);
    }
}
