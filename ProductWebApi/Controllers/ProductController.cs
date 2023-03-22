using BusinessLogic.DTOs;
using BusinessLogic.Services;
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
        public async Task<IActionResult> GetByIdProduct(int productId)
        {
            if (productId is 0 || productId <= 0) return BadRequest();
            var product = await _productService.Get(productId);
            if (product is null) return NotFound();
            return Ok(product);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByPriceProducts()
        {
            var products = await _productService.GetOrderedByPrice();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (productDto is null) return BadRequest();
            var result = await _productService.Add(productDto);
            if (result is null) return UnprocessableEntity();
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductPutDto productPutDto)
        {
            if (productPutDto is null || productPutDto.ProductId is 0) return BadRequest();
            var result = await _productService.Update(productPutDto);
            if (!result) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (productId is 0 || productId <= 0) return BadRequest();
            var result = await _productService.Remove(productId);
            if (!result) return NotFound();
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
