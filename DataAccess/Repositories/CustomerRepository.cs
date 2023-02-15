using CustomerWebApi;
using CustomerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CustomerRepository : IRepository<Customer, int>
    {
        private readonly CustomerDbContext _customerDbContext;
        public CustomerRepository(CustomerDbContext context)
        {
            _customerDbContext = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        => await _customerDbContext.Customers.OrderBy(a => a.CustomerId).ToListAsync();
            
        public async Task<Customer> Get(int customerId)
        => await _customerDbContext.Customers.FindAsync(customerId);

        public async Task<Customer> Add(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _customerDbContext.Customers.Update(customer);
            await _customerDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task Remove(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _customerDbContext.Remove(customer);
            }
        }

        public async Task Save()
        {
            await _customerDbContext.SaveChangesAsync();
        }
    }
}
