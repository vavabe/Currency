namespace CurrencyServices.CurrencyFetcher.Application.Interfaces;

public interface ICurrencyService
{
    Task<string> FetchCurrenciesAndSave();
}
