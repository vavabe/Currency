using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Polly;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Services;

public class HttpCurrencyFetchService : IHttpCurrencyFetchService
{
    private readonly CurrencyApiOptions _currencyApiOptions;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpCurrencyFetchService(IOptions<CurrencyApiOptions> options, IHttpClientFactory httpClientFactory)
    {
        _currencyApiOptions = options.Value;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Stream> GetResponseAsStream()
    {
        var retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryCount: _currencyApiOptions.Retry.Retries,
                sleepDurationProvider: (_) => TimeSpan.FromSeconds(_currencyApiOptions.Retry.DelayInSeconds));

        var httpClient = _httpClientFactory.CreateClient();

        return await httpClient.GetStreamAsync(_currencyApiOptions.ApiUrl);
    }
}
