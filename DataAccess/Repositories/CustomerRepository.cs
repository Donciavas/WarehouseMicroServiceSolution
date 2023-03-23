using DataAccess.MicroServiceDbContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private new readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(CustomerDbContext context, ILogger<CustomerRepository> logger) : base(context)
        {
            _customerDbContext = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Customer>> GetOrderedByName()
        {
            try
            {
                var customerOrderBy = await _customerDbContext.Customers!.OrderBy(n => n.CustomerName).ToListAsync();
                return customerOrderBy!;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
        public bool EmailCheck(string checkEmail)
        {
            try
            {
                var emailCheck = _customerDbContext.Customers!.Any(u => u.Email == checkEmail);
                return emailCheck!;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
    }
}
