namespace CryptoGuard.Infrastructure.Configurations;

public class CoinGeckoOptions
{
    public string BaseUrl { get; init; } = string.Empty;
    public string ApiKey { get; init; } = string.Empty;
    public int TimeoutInSeconds { get; init; }
    public int PriceCacheInMinutes { get; init; }
}