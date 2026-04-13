namespace CryptoGuard.Domain.Domains;

public class Asset
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public Currency Currency { get; private set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastUpdated { get; set; }
    
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