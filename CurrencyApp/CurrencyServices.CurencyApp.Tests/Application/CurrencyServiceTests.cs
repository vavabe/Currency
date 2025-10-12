using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Application.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurencyApp.Tests.Application
{
    public class CurrencyServiceTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock = new Mock<ICurrencyRepository>();
        private CurrencyService _currencyService;

        public CurrencyServiceTests()
        {
            _currencyService = new CurrencyService(_currencyRepositoryMock.Object);
        }

        [Fact]
        public async Task AddToFavorite_ReturnsExpected()
        {
            var userId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();
            _currencyRepositoryMock.Setup(x => x.AddToFavorite(currencyId, userId)).ReturnsAsync("1");

            var result = await _currencyService.AddToFavorite(currencyId, userId);

            result.Should().Be("1");
        }

        [Fact]
        public void AddToFavorite_RepositoryThrowsException_ThrowsException()
        {
            var userId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();
            _currencyRepositoryMock.Setup(x => x.AddToFavorite(currencyId, userId)).ThrowsAsync(new Exception());

            Func<Task<string>> addToFavoriteTask = async () => await _currencyService.AddToFavorite(currencyId, userId);

            addToFavoriteTask.Should().ThrowAsync();
        }
    }
}
