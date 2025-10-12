using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public Task<string> AddToFavorite(Guid currencyId, Guid userId) => _currencyRepository.AddToFavorite(currencyId, userId);

        public Task<IEnumerable<Currency>> GetFavoritesByUser(Guid userId) => _currencyRepository.GetFavoritesByUser(userId);
    }
}
