using Microsoft.EntityFrameworkCore;
using ProductWebApi;
using ProductWebApi.Models;

namespace DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ProductDbContext _productDbContext;
        public ProductRepository(ProductDbContext context) : base(context)
        {
            _productDbContext = context;
        }

        public async Task<IEnumerable<Product>> GetOrderedByPrice()
      => await _productDbContext.Products.OrderBy(n => n.ProductPrice).ToListAsync();
    }
}
