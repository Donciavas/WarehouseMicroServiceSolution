using Microsoft.EntityFrameworkCore;
using ProductWebApi;
using ProductWebApi.Models;

namespace DataAccess.Repositories
{
    public class ProductRepository : IRepository<Product, int>
    {
        private readonly ProductDbContext _productDbContext;
        public ProductRepository(ProductDbContext context)
        {
            _productDbContext = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        => await _productDbContext.Products.OrderBy(a => a.ProductId).ToListAsync();
            
        public async Task<Product> Get(int productId)
        => await _productDbContext.Products.FindAsync(productId);

        public async Task<Product> Add(Product product)
        {
            await _productDbContext.Products.AddAsync(product);
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _productDbContext.Products.Update(product);
            await _productDbContext.SaveChangesAsync();
            return product;
        }

        public async Task Remove(int productId)
        {
            var product = await _productDbContext.Products.FindAsync(productId);
            if (product != null)
            {
                _productDbContext.Remove(product);
            }
        }

        public async Task Save()
        {
            await _productDbContext.SaveChangesAsync();
        }
    }
}
