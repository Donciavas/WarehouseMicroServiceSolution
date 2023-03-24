using DataAccess.MicroServiceDbContexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class CustomerDBServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Database")));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
