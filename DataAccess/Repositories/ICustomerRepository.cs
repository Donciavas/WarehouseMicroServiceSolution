using CustomerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Task<IEnumerable<Customer>> GetOrderedByName();
    }
}
