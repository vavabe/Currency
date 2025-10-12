using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Application.Interfaces
{
    public interface IDapperWrapper
    {
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param);
        Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param);
    }
}
