namespace CurrencyServices.DbMigrator.Application.Interfaces;

public interface IMigratorService
{
    Task<IEnumerable<string>> ApplyMigrations(long? versionTo);
    Task<IEnumerable<string>> GetMigrationsHistory();
    Task<IEnumerable<string>> GetMigrationToApply();
    Task<IEnumerable<string>> RollbackMigrations(long versionTo);
}
