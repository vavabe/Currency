using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Application.Services;
using CurrencyServices.CurrencyApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace CurrencyServices.CurencyApp.Tests.Application;

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

    [Fact]
    public async Task GetFavoritesByUser_ReturnsExpectedCurrencies()
    {
        var userId = Guid.NewGuid();
        var expectedCurrencies = new List<Currency>
        {
            new Currency { Name = "USD", Rate = 1.0m },
            new Currency { Name = "EUR", Rate = 0.9m }
        };
        _currencyRepositoryMock.Setup(x => x.GetFavoritesByUser(userId)).ReturnsAsync(expectedCurrencies);

        var result = await _currencyService.GetFavoritesByUser(userId);

        result.Should().BeEquivalentTo(expectedCurrencies);
    }

    [Fact]
    public void GetFavoritesByUser_RepositoryThrowsException_ThrowsException()
    {
        var userId = Guid.NewGuid();
        _currencyRepositoryMock.Setup(x => x.GetFavoritesByUser(userId)).ThrowsAsync(new Exception());

        Func<Task<IEnumerable<Currency>>> getFavoritesTask = async () => await _currencyService.GetFavoritesByUser(userId);

        getFavoritesTask.Should().ThrowAsync<Exception>();
    }
}
