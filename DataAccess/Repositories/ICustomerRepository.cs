using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Task<IEnumerable<Customer>> GetOrderedByName();
        bool EmailCheck(string checkEmail);
    }
}
