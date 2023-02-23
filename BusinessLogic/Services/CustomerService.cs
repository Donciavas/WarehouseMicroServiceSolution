using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<Customer>> GetOrderedByName() =>
            await _customerRepository.GetOrderedByName();
    }
}
