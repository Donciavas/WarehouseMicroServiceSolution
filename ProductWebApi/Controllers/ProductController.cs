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
            if (productId is null || productId <= 0) return BadRequest();
            var product = await _productService.Get((int)productId);
            if (product is null) return NotFound();
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
            if (productDto is null) return BadRequest();
            var result = await _productService.Add(productDto);
            if (result is null) return UnprocessableEntity();
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            if (product is null) return BadRequest();
            var result = await _productService.Update(product);
            if (!result) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId is null || productId <= 0) return BadRequest();
            var result = await _productService.Remove((int)productId);
            if (!result) return NotFound();
            return RedirectToAction(nameof(GetProducts));
        }
    }
}
