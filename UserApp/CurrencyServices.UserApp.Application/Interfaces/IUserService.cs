using CurrencyServices.UserApp.Application.Models;

namespace CurrencyServices.UserApp.Application.Interfaces;

public interface IUserService
{
    Task<string> Login(LoginDto loginDto);
    Task<bool> Register(RegisterDto registerDto);
    Task<bool> Logout(LogoutDto logoutDto);
}
