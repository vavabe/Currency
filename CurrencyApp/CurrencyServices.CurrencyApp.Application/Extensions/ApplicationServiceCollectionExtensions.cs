using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyServices.CurrencyApp.Application.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();

            return services;
        }
    }
}
