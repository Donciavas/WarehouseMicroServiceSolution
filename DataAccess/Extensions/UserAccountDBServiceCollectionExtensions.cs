using DataAccess.MicroServiceDbContexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class UserAccountDBServiceCollectionExtensions
    {
        public static IServiceCollection AddUserAccountDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserAccountDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Database")));
            services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            return services;
        }
    }
}
