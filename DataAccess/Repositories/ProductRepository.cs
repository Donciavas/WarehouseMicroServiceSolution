using DataAccess.MicroServiceDbContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ProductDbContext _productDbContext;
        private new readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ProductDbContext context, ILogger<ProductRepository> logger) : base(context)
        {
            _productDbContext = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Product>> GetOrderedByPrice()
        {
            try
            {
                var productOrderBy = await _productDbContext.Products!.OrderBy(n => n.ProductPrice).ToListAsync();
                return productOrderBy;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return default!;
            }
        }
    }
}
