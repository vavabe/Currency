using CurrencyServices.CurrencyFetcher.Application.Interfaces;
using CurrencyServices.CurrencyFetcher.Domain.Entities;
using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using AutoMapper;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Services;

public class CbrCurrencyFetchService : ICurrencyFetchService
{
    private readonly IHttpCurrencyFetchService _httpCurrencyFetchService;
    private readonly IXmlDeserializer _xmlDeserializer;
    private readonly IMapper _mapper;

    public CbrCurrencyFetchService(IHttpCurrencyFetchService httpCurrencyFetchService,
        IXmlDeserializer xmlDeserializer,
        IMapper mapper)
    {
        _httpCurrencyFetchService = httpCurrencyFetchService;
        _xmlDeserializer = xmlDeserializer;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Currency>> FetchCurrencies()
    {
        var xmlStreamResult = await _httpCurrencyFetchService.GetResponseAsStream();

        var deserializedCurrencies = _xmlDeserializer.Deserialize(xmlStreamResult);

        return _mapper.Map<IEnumerable<Currency>>(deserializedCurrencies);
    }
}
