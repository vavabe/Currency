using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CurrencyServices.CurrencyApp.Infrastructure.Options;

public class JwtOptions
{
    public const string Name = "Jwt";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
}
