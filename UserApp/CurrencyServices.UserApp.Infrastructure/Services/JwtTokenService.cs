using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyServices.UserApp.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly ICacheService _cacheService;
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(ICacheService cacheService, IOptions<JwtOptions> options)
    {
        _cacheService = cacheService;
        _jwtOptions = options.Value;
    }

    public string GetJwtToken(Guid id, string username)
    {
        var claims = new Dictionary<string, object>
        {
            ["user_name"] = username,
            ["user_id"] = id.ToString()
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Claims = claims,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenLifetime),
            SigningCredentials = new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        }; 
        var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();
        handler.SetDefaultTimesOnTokenCreation = false;
        return handler.CreateToken(descriptor);
    }

    public async Task<bool> InvalidateToken(string token)
    {
        await _cacheService.AddAsync(token, "blacklist");
        return true;
    }
}
