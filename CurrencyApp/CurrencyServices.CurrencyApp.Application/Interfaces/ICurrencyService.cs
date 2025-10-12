using CurrencyServices.CurrencyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetFavoritesByUser(Guid userId);
        Task<string> AddToFavorite(Guid currencyId, Guid userId);
    }
}
