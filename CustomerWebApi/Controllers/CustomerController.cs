using CustomerWebApi.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {

            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();
            return Ok(customers);
        }

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetById(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.Get((int)customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByName()
        {
            var customers = await _customerRepository.GetOrderedByName();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _customerRepository.Add(customer);
            await _customerRepository.Save();
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            await _customerRepository.Update(customer);
            await _customerRepository.Save();
            return Ok(customer);
        }

        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> Delete(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.Get((int)customerId);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.Remove((int)customerId);
            await _customerRepository.Save();
            return RedirectToAction(nameof(GetCustomers));
        }
    }
}
