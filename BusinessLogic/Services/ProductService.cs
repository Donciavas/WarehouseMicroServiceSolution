using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetOrderedByPrice()
      => await _productRepository.GetOrderedByPrice();
    }
}
