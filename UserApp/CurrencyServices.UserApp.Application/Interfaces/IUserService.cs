using CurrencyServices.UserApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(LoginDto loginDto);
        Task<bool> Register(RegisterDto registerDto);
        Task<bool> Logout(LogoutDto logoutDto);
    }
}
