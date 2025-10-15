using System.Data;

namespace CurrencyServices.UserApp.Infrastructure.Interfaces;

public interface IDapperWrapper
{
    Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? param);
    Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param);
}
