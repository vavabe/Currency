using CurrencyServices.DbMigrator.Application.Interfaces;
using CurrencyServices.Migrator.Infrastructure.Options;
using CurrencyServices.Migrator.Infrastructure.Services;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CurrencyServices.Migrator.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.Name));
        services.AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetSection("Db:ConnectionString").Value)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(logging => logging
                .AddFluentMigratorConsole());

        services.AddScoped<IMigratorService, MigratorService>();
        return services;
    }
}
