using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CurrencyServices.UserApp.Infrastructure.Services
{
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
            var data = Encoding.UTF8.GetBytes("SomeStringFromConfig1234 SomeStringFromConfig1234");
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(data);
            var claims = new Dictionary<string, object>
            {
                ["user_name"] = username,
                ["user_id"] = id.ToString()
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "",
                Audience = "",
                Claims = claims,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            }; 
            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();
            handler.SetDefaultTimesOnTokenCreation = false;
            return handler.CreateToken(descriptor);
        }

        public async Task<bool> InvalidateToken(string token)
        {
            await _cacheService.AddAsync("blacklist_tokens", token);
            return true;
        }
    }
}
