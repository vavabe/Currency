using CurrencyServices.ApiGateway.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace CurrencyServices.UserApp.Application.Middlewares;

public class TokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;

    public TokenBlacklistMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICacheService cacheService)
    {
        var authorizationHeader = context.Request.Headers[HeaderNames.Authorization].ToString();
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
            }
            else
            {
                var jwt = authorizationHeader.Replace("Bearer ", "");
                if (await cacheService.ContainsValue(jwt))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized user");
                    return;
                }
            }
        }
        await _next(context);
    }
}
