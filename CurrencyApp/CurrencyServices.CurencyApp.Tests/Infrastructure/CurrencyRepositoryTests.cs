using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Infrastructure.Options;
using CurrencyServices.CurrencyApp.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurencyApp.Tests.Infrastructure
{
    public class CurrencyRepositoryTests
    {
        private readonly Mock<IDapperWrapper> _dapperWrapperMock = new Mock<IDapperWrapper>();
        private readonly Mock<IOptions<DbOptions>> _dbOptionsMock = new Mock<IOptions<DbOptions>>();
        private readonly CurrencyRepository _currencyRepository;

        public CurrencyRepositoryTests()
        {
            _dbOptionsMock.Setup(x => x.Value).Returns(new DbOptions());
            _currencyRepository = new CurrencyRepository(_dbOptionsMock.Object, 
                _dapperWrapperMock.Object);
        }

        [Fact]
        public async Task AddToFavorite_ReturnsExpected()
        {
            var userId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();
            var insertedCount = 1;

            _dapperWrapperMock.Setup(x => x.ExecuteAsync(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<object?>()))
                .ReturnsAsync(insertedCount);

            var result = await _currencyRepository.AddToFavorite(currencyId, userId);

            result.Should().Be(insertedCount.ToString());
        }

        [Fact]
        public async Task AddToFavorite_DapperThrowsException_ThrowsException()
        {
            var userId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();
            var insertedCount = 1;

            _dapperWrapperMock.Setup(x => x.ExecuteAsync(It.IsAny<IDbConnection>(), It.IsAny<string>(), It.IsAny<object?>()))
                .ThrowsAsync(new Exception());

            Func<Task<string>> result = async () => await _currencyRepository.AddToFavorite(currencyId, userId);

            result.Should().ThrowAsync();
        }
    }
}
