using System.Data;

namespace CurrencyServices.CurrencyApp.Infrastructure.Interfaces;

public interface IDapperWrapper
{
    Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param);
    Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param);
}
