using CurrencyServices.CurrencyFetcher.Application.Interfaces;
using CurrencyServices.CurrencyFetcher.Application.Services;
using CurrencyServices.CurrencyFetcher.Infrastructure.Options;
using CurrencyServices.CurrencyFetcher.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyServices.CurrencyFetcher.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BackgroundServiceOptions>(configuration.GetSection(BackgroundServiceOptions.Name));
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddHostedService<CurrencyFetcherBackgroundService>();
        return services;
        
    }
}
