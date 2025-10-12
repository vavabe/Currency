namespace CurrencyServices.CurrencyFetcher.Infrastructure.Options;

public class BackgroundServiceOptions
{
    public const string Name = "BackgroundService";
    public int DelayInMinutes { get; set; } = 10;
}
