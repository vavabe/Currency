using Dapper;
using System.Data;
using CurrencyServices.CurrencyApp.Infrastructure.Interfaces;

namespace CurrencyServices.CurrencyApp.Infrastructure.Repositories;

public class DapperWrapper : IDapperWrapper
{
    public Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param = null) 
        => connection.ExecuteAsync(sql, param);

    public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param = null)
        => connection.QueryAsync<T>(sql, param);
}
