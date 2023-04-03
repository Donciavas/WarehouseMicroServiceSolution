using DataAccess.DTOs;
using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;
        public CustomerService(ICustomerRepository customerRepository, IEmailService emailService) : base(customerRepository)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }
        public async Task<IEnumerable<Customer>> GetOrderedByName() =>
            await _customerRepository.GetOrderedByName();
        public ResponseDto GreetingEmail(string verifyEmail) =>
             _emailService.GreetingEmail(verifyEmail);
        public ResponseDto EmailCheck(string checkEmail) =>
            _customerRepository.EmailCheck(checkEmail);
    }
}
