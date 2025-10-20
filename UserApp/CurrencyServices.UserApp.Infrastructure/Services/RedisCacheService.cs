using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CurrencyServices.UserApp.Infrastructure.Services;

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

    public async Task<bool> AddAsync(string key, string value)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(value);
        await _distributedCache.SetAsync(key, bytes, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheOptions.ExpirationInMinutes)
        });
        return true;
    }
}
