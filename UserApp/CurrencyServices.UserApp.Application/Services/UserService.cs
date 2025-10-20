using CurrencyServices.UserApp.Application.Exceptions;
using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Application.Models;

namespace CurrencyServices.UserApp.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;

    public UserService(IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _userRepository.GetUser(loginDto.UserName);

        if (_passwordHasher.VerifyPassword(user.Password, loginDto.Password))
            return _jwtTokenService.GetJwtToken(user.Id, user.Name);
        else
            throw new WrongCredentialsException();
    }

    public Task<bool> Logout(LogoutDto logoutDto) => _jwtTokenService.InvalidateToken(logoutDto.Token);

    public async Task<bool> Register(RegisterDto registerDto)
    {
        var hashedPassword = _passwordHasher.HashPassword(registerDto.Password);

        await _userRepository.AddUser(registerDto.UserName, hashedPassword);

        return true;
    }
}
