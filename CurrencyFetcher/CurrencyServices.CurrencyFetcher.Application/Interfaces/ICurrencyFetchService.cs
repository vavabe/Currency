using CurrencyServices.CurrencyFetcher.Domain.Entities;

namespace CurrencyServices.CurrencyFetcher.Application.Interfaces;

public interface ICurrencyFetchService
{
    Task<IEnumerable<Currency>> FetchCurrencies();
}
