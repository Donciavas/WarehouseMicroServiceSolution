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
            if (customers is null)
                return NotFound("No customers found.");
            return Ok(customers);
        }
        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetByIdCustomer(int customerId)
        {
            if (customerId is 0 || customerId <= 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var customer = await _customerService.Get(customerId);
            if (customer is null) return NotFound($"No customer found by this No. {customerId} ID.");
            return Ok(customer);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByNameCustomers()
        {
            var customers = await _customerService.GetOrderedByName();
            if (customers is null)
                return NotFound("No customers found.");
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            if (customerDto is null) return BadRequest("Cannot create customer without any data.");
            var searchEmailInUse = _customerService.EmailCheck(customerDto.Email!);
            if (searchEmailInUse) return BadRequest("This email is in use. Try to use different email instead. ");
            _customerService.GreetingEmail(customerDto.Email!);
            var result = await _customerService.Add(customerDto);
            if (result is null) return UnprocessableEntity("Internal server error.Something went wrong while trying to add customer to database.");
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerPutDto customerPutDto)
        {
            if (customerPutDto is null || customerPutDto.CustomerId is 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var result = await _customerService.Update(customerPutDto);
            if (!result) return NotFound("No customer found by this ID.");
            return Ok(result);
        }
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            if (customerId is 0 || customerId <= 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var result = await _customerService.Remove(customerId);
            if (!result) return NotFound($"No customer found by No. {customerId} ID.");
            return RedirectToAction(nameof(GetCustomers));
        }
    }
}
