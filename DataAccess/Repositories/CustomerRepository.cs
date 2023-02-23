using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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
      => await _customerDbContext.Customers!.OrderBy(n => n.CustomerName).ToListAsync();
    }
}
