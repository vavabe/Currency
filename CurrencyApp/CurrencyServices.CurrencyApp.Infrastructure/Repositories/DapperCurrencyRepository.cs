using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Domain.Entities;
using CurrencyServices.CurrencyApp.Infrastructure.Interfaces;
using CurrencyServices.CurrencyApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CurrencyServices.CurrencyApp.Infrastructure.Repositories;

public class DapperCurrencyRepository : ICurrencyRepository
{
    private readonly DbOptions _dbOptions;
    private readonly IDapperWrapper _dapper;

    public DapperCurrencyRepository(IOptions<DbOptions> options, IDapperWrapper dapper)
    {
        _dbOptions = options.Value;
        _dapper = dapper;
    }

    public async Task<string> AddToFavorite(Guid currencyId, Guid userId)
    {
        const string sql = @"
                INSERT INTO ""Favorite"" (""CurrencyId"", ""UserId"") 
                VALUES (@CurrencyId, @UserId);
            ";
        await using var connection = new NpgsqlConnection(_dbOptions.ConnectionString);
        var result = await _dapper.ExecuteAsync(connection, sql, new { CurrencyId = currencyId, UserId = userId });
        return result.ToString();
    }

    public async Task<IEnumerable<Currency>> GetFavoritesByUser(Guid userId)
    {
        const string sql = @"SELECT c.""Id"", c.""Name"", c.""Rate"" FROM ""Currency"" c
                    INNER JOIN ""Favorite"" f ON c.""Id"" = f.""CurrencyId""
                    WHERE f.""UserId"" = @UserId";
        await using var connection = new NpgsqlConnection(_dbOptions.ConnectionString);
        return await _dapper.QueryAsync<Currency>(connection, sql, new { UserId = userId });
    }
}
