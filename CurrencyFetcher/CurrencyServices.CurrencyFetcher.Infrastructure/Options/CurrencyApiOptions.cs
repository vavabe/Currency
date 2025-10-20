namespace CurrencyServices.CurrencyFetcher.Infrastructure.Options;

public class CurrencyApiOptions
{
    public const string Name = "CurrencyApi";
    public string ApiUrl { get; set; } = string.Empty;
    public RetryOptions Retry { get; set; } = new();
}

public class RetryOptions
{
    public int Retries { get; set; } = 3;
    public int DelayInSeconds { get; set; } = 10;
}
