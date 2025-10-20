
using Dapper;
using System.Data;
using CurrencyServices.UserApp.Infrastructure.Interfaces;

namespace CurrencyServices.UserApp.Infrastructure.Repositories;

public class DapperWrapper : IDapperWrapper
{
    public Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param = null) 
        => connection.ExecuteAsync(sql, param);

    public Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? param = null)
        => connection.QuerySingleAsync<T>(sql, param);
}
