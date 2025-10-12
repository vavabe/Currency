using CurrencyServices.CurrencyFetcher.Application.Interfaces;
using CurrencyServices.CurrencyFetcher.Domain.Entities;
using CurrencyServices.CurrencyFetcher.Infrastructure.Options;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly DbOptions _dbOptions;

    public CurrencyRepository(IOptions<DbOptions> options)
    {
        _dbOptions = options.Value;
    }

    public async Task<string> AddCurrenciesAsync(IEnumerable<Currency> currencies)
    {
        var sql = @"
                INSERT INTO ""Currency"" (""Name"", ""Rate"") 
                VALUES (@Name, @Rate)
                ON CONFLICT (""Name"") DO UPDATE 
                SET ""Rate"" = EXCLUDED.""Rate"";
            ";

        using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
        {
            var result = await connection.ExecuteAsync(sql, currencies);
            return result.ToString();
        }
    }
}
