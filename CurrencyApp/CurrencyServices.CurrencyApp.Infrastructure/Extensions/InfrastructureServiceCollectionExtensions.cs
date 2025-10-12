
using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Infrastructure.Options;
using CurrencyServices.CurrencyApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Name));
            services.Configure<DbOptions>(configuration.GetSection(DbOptions.Name));

            services.AddScoped<IDapperWrapper, DapperWrapper>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            return services;
        }
    }
}
