using CurrencyServices.CurrencyFetcher.Domain.Entities;

namespace CurrencyServices.CurrencyFetcher.Application.Interfaces;

public interface ICurrencyRepository
{
    Task<string> AddCurrenciesAsync(IEnumerable<Currency> currencies);
}
