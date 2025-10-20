namespace CurrencyServices.CurrencyApp.Infrastructure.Options;

public class DbOptions
{
    public const string Name = "Db";
    public string ConnectionString { get; set; } = string.Empty;
}
