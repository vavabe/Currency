using CurrencyServices.ApiGateway.Infrastructure.Services;
using CurrencyServices.ApiGateway.Interfaces;
using CurrencyServices.ApiGateway.Options;
using CurrencyServices.UserApp.Application.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace CurrencyServices.ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddOcelot();
        builder.Services
            .AddOcelot(builder.Configuration);
        builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(CacheOptions.Name));
        builder.Services.AddScoped<ICacheService, RedisCacheService>();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            var redisOptions = builder.Configuration.GetSection(CacheOptions.Name).Get<CacheOptions>()!;
            options.Configuration = redisOptions.ConnectionString;
            options.InstanceName = redisOptions.InstanceName;
        });
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerForOcelot(builder.Configuration);
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateway" });
            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
        });

        var app = builder.Build();

        app.UseMiddleware<TokenBlacklistMiddleware>();

        app.UseSwaggerForOcelotUI();
        app.UseOcelot();


        app.Run();
    }
}
