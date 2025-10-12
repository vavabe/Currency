using CurrencyServices.UserApp.Application.Interfaces;
using CurrencyServices.UserApp.Domain.Entities;
using CurrencyServices.UserApp.Infrastructure.Options;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbOptions _dbOptions;

        public UserRepository(IOptions<DbOptions> options)
        {
            _dbOptions = options.Value;
        }

        public async Task<string> AddUser(string username, string passwordHash)
        {
            var sql = @"
                INSERT INTO ""User"" (""Name"", ""Password"")
                VALUES (@Name, @Password)
            ";
            using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
            {
                var result = await connection.ExecuteAsync(sql, new { Name = username, Password = passwordHash });
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
                var result = await connection.QuerySingleAsync<User>(sql, new { Username = username });
                return result;
            }
        }
    }
}
