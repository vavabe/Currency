using FluentMigrator;

namespace CurrencyServices.Migrator.Infrastructure;

[Migration(2025100501)]
public class AddFavoritesTable : Migration
{
    public override void Up()
    {
        Create.Table("Favorite")
            .WithColumn("UserId").AsGuid()
            .WithColumn("CurrencyId").AsGuid();

        Create.PrimaryKey("pk_favorite")
            .OnTable("Favorite")
            .Columns("UserId", "CurrencyId");

        Create.ForeignKey("fk_Favorite_UserId_User_Id")
            .FromTable("Favorite").ForeignColumn("UserId")
            .ToTable("User").PrimaryColumn("Id");

        Create.ForeignKey("fk_Favorite_CurrenctId_Currency_Id")
            .FromTable("Favorite").ForeignColumn("CurrencyId")
            .ToTable("Currency").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Favorite");
    }
}
