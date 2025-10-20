using FluentMigrator;

namespace CurrencyServices.Migrator.Infrastructure.Migrations;

[Migration(2025111000)]
public class AlterUserNameUnique : Migration
{
    public override void Up()
    {
        Create.Index("ix_user_name_unique")
            .OnTable("User")
            .OnColumn("Name")
            .Unique();
    }

    public override void Down()
    {
        Delete.Index("ix_user_name_unique")
            .OnTable("User");
    }
}
