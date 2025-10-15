using CurrencyServices.CurrencyFetcher.Application.Extensions;
using CurrencyServices.CurrencyFetcher.Infrastructure.Extensions;
using CurrencyServices.CurrencyFetcher.Middlewares;

namespace CurrencyServices.CurrencyFetcher;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices(builder.Configuration);

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.Run();
    }
}
