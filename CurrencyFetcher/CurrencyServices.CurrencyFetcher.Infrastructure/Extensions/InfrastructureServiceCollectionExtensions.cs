using CurrencyServices.CurrencyFetcher.Application.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Options;
using CurrencyServices.CurrencyFetcher.Infrastructure.Repositories;
using CurrencyServices.CurrencyFetcher.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        services.AddSerilog();

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
