namespace CurrencyServices.UserApp.Application.Interfaces;

public interface IJwtTokenService
{
    string GetJwtToken(Guid id, string username);
    Task<bool> InvalidateToken(string token);
}
