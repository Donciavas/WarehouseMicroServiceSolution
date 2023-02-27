using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Services
{
    public interface ICustomerService : IService<Customer, int>
    {
        Task<IEnumerable<Customer>> GetOrderedByName();
        bool GreetingEmail(string verifyEmail);
        bool EmailCheck(string checkEmail);
    }
}
