using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Extensions
{
    public static class UserAccountServiceCollectionExtensions
    {
        public static IServiceCollection AddUserAccountBusinessLogic(this IServiceCollection services)
        {

            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            return services;
        }
    }
}
