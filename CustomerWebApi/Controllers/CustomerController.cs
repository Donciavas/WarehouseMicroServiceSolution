using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetAll();
            return Ok(customers);
        }
        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetById(int? customerId)
        {
            if (customerId is null || customerId <= 0) return BadRequest();
            var customer = await _customerService.Get((int)customerId);
            if (customer is null) return NotFound();
            return Ok(customer);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByName()
        {
            var customers = await _customerService.GetOrderedByName();
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            if (customerDto is null) return BadRequest();
            var result = await _customerService.Add(customerDto);
            if (result is null) return UnprocessableEntity();
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (customer is null) return BadRequest();
            var result = await _customerService.Update(customer);
            if (!result) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> Delete(int? customerId)
        {
            if (customerId is null || customerId <= 0) return BadRequest();
            var result = await _customerService.Remove((int)customerId);
            if (!result) return NotFound();
            return RedirectToAction(nameof(GetCustomers));
        }
    }
}
