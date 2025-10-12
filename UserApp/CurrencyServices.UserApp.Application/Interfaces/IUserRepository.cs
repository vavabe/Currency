using CurrencyServices.UserApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username);
        Task<string> AddUser(string username, string passwordHash);
    }
}
