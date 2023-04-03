using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            if (customerId is 0 || customerId <= 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var customer = await _customerService.Get(customerId);
            if (customer is null) return NotFound($"No customer found by this No. {customerId} ID.");
            return Ok(customer);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByNameCustomers()
        {
            var customers = await _customerService.GetOrderedByName();
            if (customers.IsNullOrEmpty())
                return NotFound("No customers found.");
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            if (customerDto is null) return BadRequest("Cannot create customer without any data.");
            var searchEmailInUse = _customerService.EmailCheck(customerDto.Email!);
            if (searchEmailInUse.IsSuccess) return BadRequest(searchEmailInUse.Message);
            _customerService.GreetingEmail(customerDto.Email!);
            var response = await _customerService.Add(customerDto);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);
            return StatusCode(201, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerPutDto customerPutDto)
        {
            if (customerPutDto is null || customerPutDto.CustomerId is 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var response = await _customerService.Update(customerPutDto);
            if (!response.IsSuccess) return NotFound(response.Message);
            return Ok(response);
        }
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            if (customerId is 0 || customerId <= 0) return BadRequest("Customer ID starts with No. 1 and cannot be empty data.");
            var response = await _customerService.Remove(customerId);
            if (!response.IsSuccess) return NotFound(response.Message);
            return RedirectToAction(nameof(GetCustomers));
        }
    }
}
