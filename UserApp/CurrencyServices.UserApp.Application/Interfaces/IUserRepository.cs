using CurrencyServices.UserApp.Domain.Entities;

namespace CurrencyServices.UserApp.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetUser(string username);
    Task<string> AddUser(string username, string passwordHash);
}
