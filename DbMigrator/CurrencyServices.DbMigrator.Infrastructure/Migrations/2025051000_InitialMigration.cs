using FluentMigrator;

namespace CurrencyServices.Migrator.Infrastructure;

[Migration(2025100500)]
public class InitialMigration : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\"");

        Create.Table("User")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("Name").AsString()
            .WithColumn("Password").AsString();

        Create.Table("Currency")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("Name").AsString()
            .WithColumn ("Rate").AsDecimal();
    }

    public override void Down()
    {
        Delete.Table("User");
        Delete.Table("Currency");
    }
}
