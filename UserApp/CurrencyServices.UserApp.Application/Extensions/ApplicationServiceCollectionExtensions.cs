using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyServices.UserApp.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
