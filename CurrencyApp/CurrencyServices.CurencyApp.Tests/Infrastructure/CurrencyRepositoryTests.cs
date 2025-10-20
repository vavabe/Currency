using CurrencyServices.CurrencyApp.Domain.Entities;
using CurrencyServices.CurrencyApp.Infrastructure.Interfaces;
using CurrencyServices.CurrencyApp.Infrastructure.Options;
using CurrencyServices.CurrencyApp.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Data;

namespace CurrencyServices.CurencyApp.Tests.Infrastructure;

public class CurrencyRepositoryTests
{
    private readonly Mock<IDapperWrapper> _dapperWrapperMock = new Mock<IDapperWrapper>();
    private readonly Mock<IOptions<DbOptions>> _dbOptionsMock = new Mock<IOptions<DbOptions>>();
    private readonly DapperCurrencyRepository _repository;

    public CurrencyRepositoryTests()
    {
        _dbOptionsMock.Setup(x => x.Value).Returns(new DbOptions());
        _repository = new DapperCurrencyRepository(_dbOptionsMock.Object, _dapperWrapperMock.Object);
    }

    [Fact]
    public async Task AddToFavorite_ReturnsExpectedResult()
    {
        var userId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();
        var expected = 1;

        _dapperWrapperMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<object?>()))
            .ReturnsAsync(expected);

        var result = await _repository.AddToFavorite(currencyId, userId);

        result.Should().Be(expected.ToString());
    }

    [Fact]
    public async Task AddToFavorite_WhenDapperThrows_ThrowsException()
    {
        var userId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();

        _dapperWrapperMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<object?>()))
            .ThrowsAsync(new Exception("DB error"));

        Func<Task> act = async () => await _repository.AddToFavorite(currencyId, userId);

        await act.Should().ThrowAsync<Exception>().WithMessage("DB error");
    }

    [Fact]
    public async Task GetFavoritesByUser_ReturnsExpectedCurrencies()
    {
        var userId = Guid.NewGuid();
        var expected = new List<Currency>
        {
            new Currency { Name = "USD", Rate = 90.5m },
            new Currency { Name = "EUR", Rate = 100.1m }
        };

        _dapperWrapperMock
            .Setup(x => x.QueryAsync<Currency>(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<object?>()))
            .ReturnsAsync(expected);

        var result = await _repository.GetFavoritesByUser(userId);

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetFavoritesByUser_WhenDapperThrows_ThrowsException()
    {
        var userId = Guid.NewGuid();

        _dapperWrapperMock
            .Setup(x => x.QueryAsync<Currency>(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<object?>()))
            .ThrowsAsync(new Exception("Query error"));

        Func<Task> act = async () => await _repository.GetFavoritesByUser(userId);

        await act.Should().ThrowAsync<Exception>().WithMessage("Query error");
    }
}
