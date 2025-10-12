using CurrencyServices.CurrencyFetcher.Application.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Options;
using CurrencyServices.CurrencyFetcher.Infrastructure.Repositories;
using CurrencyServices.CurrencyFetcher.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.Name));
        services.Configure<CurrencyApiOptions>(configuration.GetSection(CurrencyApiOptions.Name));

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        services.AddHttpClient();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IXmlDeserializer, CbrXmlDeserializer>();
        services.AddScoped<IHttpCurrencyFetchService, HttpCurrencyFetchService>();
        services.AddScoped<ICurrencyFetchService, CbrCurrencyFetchService>();
        return services;

    }
}
