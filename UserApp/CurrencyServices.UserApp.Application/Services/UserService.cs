using CurrencyServices.UserApp.Application.Exceptions;
using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Application.Services
{
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

        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.GetUser(username);

            if (_passwordHasher.VerifyPassword(user.Password, password))
                return _jwtTokenService.GetJwtToken(user.Id, user.Name);
            else
                throw new WrongCredentialsException();
        }

        public Task<bool> Logout(string token) => _jwtTokenService.InvalidateToken(token);

        public async Task<bool> Register(string username, string password)
        {
            var hashedPassword = _passwordHasher.HashPassword(password);

            await _userRepository.AddUser(username, password);

            return true;
        }
    }
}
