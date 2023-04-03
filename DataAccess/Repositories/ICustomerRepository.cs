using DataAccess.DTOs;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Task<IEnumerable<Customer>> GetOrderedByName();
        ResponseDto EmailCheck(string checkEmail);
    }
}
