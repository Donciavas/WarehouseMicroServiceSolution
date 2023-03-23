using DataAccess.MicroServiceDbContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        {
            try
            {
                var productOrderBy = await _productDbContext.Products!.OrderBy(n => n.ProductPrice).ToListAsync();
                _logger!.LogInformation("Returned products ordered by price.");
                return productOrderBy!;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
    }
}
