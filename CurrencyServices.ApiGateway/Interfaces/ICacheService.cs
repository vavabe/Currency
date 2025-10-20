
namespace CurrencyServices.ApiGateway.Interfaces
{
    public interface ICacheService
    {
        Task<bool> ContainsValue(string jwt);
    }
}
