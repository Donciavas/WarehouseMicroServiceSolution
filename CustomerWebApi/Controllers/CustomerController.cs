using BusinessLogic.DTOs;
using BusinessLogic.Services;
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
        public async Task<IActionResult> GetByIdCustomer(int customerId)
        {
            if (customerId is 0 || customerId <= 0) return BadRequest();
            var customer = await _customerService.Get(customerId);
            if (customer is null) return NotFound();
            return Ok(customer);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByNameCustomers()
        {
            var customers = await _customerService.GetOrderedByName();
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            if (customerDto is null) return BadRequest();
            var searchEmailInUse = _customerService.EmailCheck(customerDto.Email!);
            if (searchEmailInUse) return BadRequest("This email is in use. Try to use different email instead. ");
            _customerService.GreetingEmail(customerDto.Email!);
            var result = await _customerService.Add(customerDto);
            if (result is null) return UnprocessableEntity();
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerPutDto customerPutDto)
        {
            if (customerPutDto is null || customerPutDto.CustomerId is 0) return BadRequest();
            var result = await _customerService.Update(customerPutDto);
            if (!result) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            if (customerId is 0 || customerId <= 0) return BadRequest();
            var result = await _customerService.Remove(customerId);
            if (!result) return NotFound();
            return RedirectToAction(nameof(GetCustomers));
        }
    }
}
