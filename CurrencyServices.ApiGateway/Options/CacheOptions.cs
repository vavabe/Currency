namespace CurrencyServices.ApiGateway.Options
{
    public class CacheOptions
    {  
        public const string Name = "Cache";
        public string ConnectionString { get; set; } = string.Empty;
        public string InstanceName { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; }
    }
}
