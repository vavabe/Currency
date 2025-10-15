namespace CurrencyServices.UserApp.Application.Interfaces;

public interface ICacheService
{
    Task<bool> AddAsync(string key, string value);
    Task<bool> ContainsValue(string key);
}
