using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Domain.Entities;
using CurrencyServices.UserApp.Infrastructure.Interfaces;
using CurrencyServices.UserApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace CurrencyServices.UserApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbOptions _dbOptions;
    private readonly IDapperWrapper _dapperWrapper;

    public UserRepository(IOptions<DbOptions> options,
        IDapperWrapper dapperWrapper)
    {
        _dbOptions = options.Value;
        _dapperWrapper = dapperWrapper;
    }

    public async Task<string> AddUser(string username, string passwordHash)
    {
        var sql = @"
                INSERT INTO ""User"" (""Name"", ""Password"")
                VALUES (@Name, @Password)
            ";
        using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
        {
            var result = await _dapperWrapper.ExecuteAsync(connection, sql, new { Name = username, Password = passwordHash });
            return result.ToString();
        }
    }

    public async Task<User> GetUser(string username)
    {
        var sql = @"
                SELECT ""Name"", ""Password"" from ""User""
                WHERE ""Name"" = @Username
            ";

        using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
        {
            var result = await _dapperWrapper.QuerySingleAsync<User>(connection, sql, new { Username = username });
            return result;
        }
    }
}
