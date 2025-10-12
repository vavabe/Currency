using CurrencyServices.CurrencyApp.Application.Interfaces;
using CurrencyServices.CurrencyApp.Domain.Entities;
using CurrencyServices.CurrencyApp.Infrastructure.Options;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace CurrencyServices.CurrencyApp.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DbOptions _dbOptions;

        public CurrencyRepository(IOptions<DbOptions> options)
        {
            _dbOptions = options.Value;
        }

        public async Task<string> AddToFavorite(Guid currencyId, Guid userId)
        {
            var sql = @"
                INSERT INTO ""Favorite"" (""CurrencyId"", ""UserId"") 
                VALUES (@CurrencyId, @UserId);
            ";
            using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql, new { CurrencyId = currencyId, UserId = userId });
                return result.ToString();
            }
        }

        public async Task<IEnumerable<Currency>> GetFavoritesByUser(Guid userId)
        {
            var sql = @"SELECT ""Name"", ""Rate"" from ""Currency"" c 
                    JOIN ""Favorite"" f ON c.""Id"" = f.""CurrencyId"" 
                    WHERE f.""UserId"" = @UserId";
            using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
            {
                return await connection.QueryAsync<Currency>(sql, new { UserId = userId });
            }
        }
    }
}
