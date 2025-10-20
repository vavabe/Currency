using CurrencyServices.UserApp.Application.Interfaces;

namespace CurrencyServices.UserApp.Infrastructure.Services;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string passwordHash, string password) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
