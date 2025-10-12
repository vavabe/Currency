using CurrencyServices.Migrator.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

namespace CurrencyServices.DbMigrator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Database Migration API",
                Version = "v1",
                Description = "API для управления миграциями базы данных"
            });
        });

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddControllers();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Database Migration API v1");
            });
        }
        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
