using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            if (products.IsNullOrEmpty())
                return NotFound("No products found.");
            return Ok(products);
        }
        [HttpGet("{ProductId:int}")]
        public async Task<IActionResult> GetByIdProduct(int productId)
        {
            if (productId is 0 || productId <= 0) return BadRequest("Product ID starts with No. 1 and cannot be empty data.");
            var product = await _productService.Get(productId);
            if (product is null) return NotFound($"No products found by this No. {productId} ID.");
            return Ok(product);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByPriceProducts()
        {
            var products = await _productService.GetOrderedByPrice();
            if (products.IsNullOrEmpty())
                return NotFound("No products found.");
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (productDto is null) return BadRequest("Cannot create product without any data.");
            var result = await _productService.Add(productDto);
            if (result is null) return UnprocessableEntity("Internal server error.Something went wrong while trying to add product to database.");
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductPutDto productPutDto)
        {
            if (productPutDto is null || productPutDto.ProductId is 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var result = await _productService.Update(productPutDto);
            if (!result) return NotFound("No product found by this ID.");
            return Ok(result);
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (productId is 0 || productId <= 0) return BadRequest("Product ID starts with No. 1 and cannot be empty data.");
            var result = await _productService.Remove(productId);
            if (!result) return NotFound($"No product found by No. {productId} ID.");
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
