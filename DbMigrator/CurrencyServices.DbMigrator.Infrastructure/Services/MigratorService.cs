using CurrencyServices.DbMigrator.Application.Interfaces;
using CurrencyServices.Migrator.Infrastructure.Options;
using Dapper;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Reflection;

namespace CurrencyServices.Migrator.Infrastructure.Services;

public class MigratorService : IMigratorService
{
    private readonly IMigrationRunner _migrationRunner;
    private readonly DbOptions _dbOptions;

    public MigratorService(IMigrationRunner migrationRunner, IOptions<DbOptions> options)
    {
        _migrationRunner = migrationRunner;
        _dbOptions = options.Value;
    }

    public async Task<IEnumerable<string>> ApplyMigrations(long? versionTo)
    {
        var migrationBefore = await GetMigrationsHistory();
        _migrationRunner.MigrateUp();
        var migrationAfter = await GetMigrationsHistory();

        return migrationAfter.Except(migrationBefore);
    }
    public async Task<IEnumerable<string>> RollbackMigrations(long versionTo)
    {
        var migrationBefore = await GetMigrationsHistory();
        _migrationRunner.RollbackToVersion(versionTo);
        var migrationAfter = await GetMigrationsHistory();

        return migrationBefore.Except(migrationAfter);
    }

    public async Task<IEnumerable<string>> GetMigrationsHistory()
    {
        try
        {
            using (IDbConnection connection = new NpgsqlConnection(_dbOptions.ConnectionString))
            {
                var appliedMigrations = await connection.QueryAsync<string>(
                    "SELECT \"Version\" FROM public.\"VersionInfo\" ORDER BY \"AppliedOn\" DESC");

                return appliedMigrations.ToList();
            }
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    public async Task<IEnumerable<string>> GetMigrationToApply()
    {
        var allMigrations = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Migration)) && type.GetCustomAttributes(typeof(MigrationAttribute), false).Any());

        var allMigrationVersions = allMigrations.Select(x => x.GetCustomAttribute(typeof(MigrationAttribute), false))
            .Cast<MigrationAttribute>()
            .Select(x => x.Version.ToString());

        var appliedMigrations = await GetMigrationsHistory();

        return allMigrationVersions.Except(appliedMigrations).OrderByDescending(x => x);
    }
}
