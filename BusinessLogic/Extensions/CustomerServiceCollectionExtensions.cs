using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Extensions
{
    public static class CustomerServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerBusinessLogic(this IServiceCollection services)
        {

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
