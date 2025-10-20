using Microsoft.AspNetCore.Authentication;

namespace CurrencyServices.UserApp.RestApi.Extensions;

public static class HttpContextJwtExtensions
{
    public static async Task<string> ExtractJwtToken(this HttpContext context)
    {
        return await context.GetTokenAsync("access_token");
    }
}
