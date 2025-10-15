using System.Security.Claims;

namespace CurrencyServices.CurrencyApp.RestApi.Extensions;

public static class HttpContextJwtExtensions
{
    public static Guid? ExtractUserId(this HttpContext context)
    {
        var claims = context.User.Identity as ClaimsIdentity;
        if (claims != null)
        {
            var userId = claims.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (userId != null && Guid.TryParse(userId, out Guid guidUserId))
            {
                return guidUserId;
            }
        }
        return null;
    }
}
