namespace CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;

public interface IHttpCurrencyFetchService
{
    Task<Stream> GetResponseAsStream();
}
