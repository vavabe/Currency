using System.Security.Claims;

namespace CurrencyServices.CurrencyApp.RestApi.Helpers;

public static class JwtUserIdExtractor
{
    public static Guid? ExtractUserId(HttpContext context)
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
