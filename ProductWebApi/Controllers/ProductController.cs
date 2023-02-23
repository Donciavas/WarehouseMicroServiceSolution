using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }
        [HttpGet("{ProductId:int}")]
        public async Task<IActionResult> GetById(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }
            var product = await _productService.Get((int)productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByPrice()
        {
            var products = await _productService.GetOrderedByPrice();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            await _productService.Add(productDto);
            return Ok(productDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _productService.Update(product);
            return Ok(product);
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }
            var Product = await _productService.Get((int)productId);
            if (Product == null)
            {
                return NotFound();
            }
            await _productService.Remove((int)productId);
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
