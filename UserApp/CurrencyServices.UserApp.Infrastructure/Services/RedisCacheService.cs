using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache,
            IOptions<CacheOptions> options)
        {
            _distributedCache = distributedCache;
        }

        public async Task<bool> AddAsync(string key, string value)
        {
            await _distributedCache.SetStringAsync(key, value);
            return true;
        }
    }
}
