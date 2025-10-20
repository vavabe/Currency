
using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Infrastructure.Interfaces;
using CurrencyServices.CurrencyApp.Infrastructure.Options;
using CurrencyServices.CurrencyApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CurrencyServices.CurrencyApp.Infrastructure.Extensions;

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
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Name));
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.Name));

        services.AddScoped<IDapperWrapper, DapperWrapper>();
        services.AddScoped<ICurrencyRepository, DapperCurrencyRepository>();

        return services;
    }
}
