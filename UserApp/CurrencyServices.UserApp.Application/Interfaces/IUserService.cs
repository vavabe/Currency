using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string username, string password);
        Task<bool> Register(string username, string password);
        Task<bool> Logout(string token);
    }
}
