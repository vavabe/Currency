using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using CurrencyServices.UserApp.Infrastructure.Repositories;
using CurrencyServices.UserApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptions>(configuration.GetSection(DbOptions.Name));
            services.Configure<CacheOptions>(configuration.GetSection(CacheOptions.Name));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Name));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddStackExchangeRedisCache(options =>
            {
                var redisOptions = configuration.GetSection(CacheOptions.Name).Get<CacheOptions>()!;
                options.Configuration = redisOptions.ConnectionString;
                options.InstanceName = redisOptions.InstanceName;
            });

            return services;
        }
    }
}
