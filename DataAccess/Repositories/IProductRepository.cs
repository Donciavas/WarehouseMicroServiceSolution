using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetOrderedByPrice();
    }
}
