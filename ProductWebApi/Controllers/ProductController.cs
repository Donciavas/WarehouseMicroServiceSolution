using CustomerWebApi.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IRepository<Product, int> _productRepository;

        public ProductController(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetById(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _productRepository.Get((int)productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productRepository.Add(product);
            await _productRepository.Save();
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _productRepository.Update(product);
            await _productRepository.Save();
            return Ok(product);
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _productRepository.Get((int)productId);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Remove((int)productId);
            await _productRepository.Save();
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
