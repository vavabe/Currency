using FluentMigrator;

namespace CurrencyServices.Migrator.Infrastructure;

[Migration(2025100502)]
public class AlterCurrencyNameUnique : Migration
{
    public override void Up()
    {
        Create.Index("ix_unique_Currency_Name")
            .OnTable("Currency")
            .OnColumn("Name")
            .Unique();
    }

    public override void Down()
    {
        Delete.Index("ix_unique_Currency_Name")
            .OnTable("Currency");
    }
}
