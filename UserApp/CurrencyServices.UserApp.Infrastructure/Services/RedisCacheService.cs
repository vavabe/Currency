using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Infrastructure.Services
{
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
            await _distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheOptions.ExpirationInMinutes)
            });
            return true;
        }

        public async Task<bool> ContainsValue(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            return !string.IsNullOrEmpty(value);
        }
    }
}
