using CurrencyServices.CurrencyFetcher.Application.Interfaces;

namespace CurrencyServices.CurrencyFetcher.Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyService;
    private readonly ICurrencyFetchService _currencyFetchService;

    public CurrencyService(ICurrencyRepository currencyService, ICurrencyFetchService currencyFetchService)
    {
        _currencyService = currencyService;
        _currencyFetchService = currencyFetchService;
    }

    public async Task<string> FetchCurrenciesAndSave()
    {
        var fetchedCurrencies = await _currencyFetchService.FetchCurrencies();

        var saveResult = await _currencyService.AddCurrenciesAsync(fetchedCurrencies);

        return saveResult;
    }
}
