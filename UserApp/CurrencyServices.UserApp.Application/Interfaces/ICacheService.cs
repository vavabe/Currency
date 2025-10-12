using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Application.Interfaces
{
    public interface ICacheService
    {
        Task<bool> AddAsync(string key, string value);
        Task<bool> ContainsValue(string key);
    }
}
