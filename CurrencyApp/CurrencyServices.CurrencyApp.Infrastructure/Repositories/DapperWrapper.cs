using CurrencyServices.CurrencyApp.Application.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Infrastructure.Repositories
{
    public class DapperWrapper : IDapperWrapper
    {
        public Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param = null) 
            => connection.ExecuteAsync(sql, param);

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param = null)
            => connection.QueryAsync<T>(sql, param);
    }
}
