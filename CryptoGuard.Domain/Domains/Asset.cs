namespace CryptoGuard.Domain.Domains;

public class Asset
{
    public Guid Id { get; private set; }
    public string Symbol { get; private set; }
    public string Name { get; private set; }
    public Currency Currency { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public DateTime LastUpdated { get; private set; }
    
    public Asset(Guid id, string symbol, string name, Currency currency, decimal currentPrice, DateTime lastUpdated)
    {
        Id = id;
        Symbol = symbol;
        Name = name;
        Currency = currency;
        CurrentPrice = currentPrice;
        LastUpdated = lastUpdated;
    }
}