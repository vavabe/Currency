using CurrencyServices.CurrencyApp.Domain.Entities;

namespace CurrencyServices.CurrencyApp.Application.Interfaces;

public interface ICurrencyRepository
{
    Task<IEnumerable<Currency>> GetFavoritesByUser(Guid userId);
    Task<string> AddToFavorite(Guid currencyId, Guid userId);
}
