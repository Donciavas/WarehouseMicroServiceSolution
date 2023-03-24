using DataAccess.MicroServiceDbContexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class ProductDBServiceCollectionExtensions
    {
        public static IServiceCollection AddProductDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(o => o.UseMySQL(configuration.GetConnectionString("Database")));
            services.AddTransient<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
