using DataAccess.MicroServiceDbContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        public CustomerRepository(CustomerDbContext context) : base(context)
        {
            _customerDbContext = context;
        }
        public async Task<IEnumerable<Customer>> GetOrderedByName()
        {
            try
            {
                var customerOrderBy = await _customerDbContext.Customers!.OrderBy(n => n.CustomerName).ToListAsync();
                _logger!.LogInformation("Returned customers ordered by name.");
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
                _logger!.LogInformation("Returned customers ordered by name.");
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
