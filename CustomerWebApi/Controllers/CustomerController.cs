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
            if (customerId == null)
            {
                return NotFound();
            }
            var customer = await _customerService.Get((int)customerId);
            if (customer == null)
            {
                return NotFound();
            }
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
            await _customerService.Add(customerDto);
            return Ok(customerDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            await _customerService.Update(customer);
            return Ok(customer);
        }
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> Delete(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }
            var customer = await _customerService.Get((int)customerId);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerService.Remove((int)customerId);
            return RedirectToAction(nameof(GetCustomers));
        }
    }
}
