using CurrencyServices.ApiGateway.Interfaces;
using CurrencyServices.ApiGateway.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CurrencyServices.ApiGateway.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly CacheOptions _cacheOptions;

    public RedisCacheService(IDistributedCache distributedCache,
        IOptions<CacheOptions> options)
    {
        _distributedCache = distributedCache;
        _cacheOptions = options.Value;
    }

    public async Task<bool> ContainsValue(string key)
    {
        var valueBytes = await _distributedCache.GetAsync(key);
        if (valueBytes == null || valueBytes.Length == 0)
            return false;

        var value = System.Text.Encoding.UTF8.GetString(valueBytes);
        return !string.IsNullOrEmpty(value);
    }
}
