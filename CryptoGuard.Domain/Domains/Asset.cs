using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Domain.Domains;

public class Asset
{
    public Guid Id { get; private set; }
    public Symbol Symbol { get; private set; }
    public string Name { get; private set; }
    public Currency Currency { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public DateTime LastUpdated { get; private set; }
    
    public Asset(Guid id, Symbol symbol, string name, Currency currency, decimal currentPrice, DateTime lastUpdated)
    {
        Id = id;
        Symbol = symbol;
        Name = name;
        Currency = currency;
        CurrentPrice = currentPrice;
        LastUpdated = lastUpdated;
    }
    
    public Result<Asset> UpdateCurrentPrice(decimal newPrice)
    {
        if (newPrice < 0) 
        {
            return Result.Failure(AssetErrors.NegativePrice);
        }

        if (IsPriceJumpTooBig(newPrice))
        {
            return Result.Failure(AssetErrors.PriceJumpTooBig);
        }
        
        if (newPrice == CurrentPrice)
        {
            return Result.Failure(AssetErrors.SamePrice);
        }

        CurrentPrice = newPrice;
        LastUpdated = DateTime.UtcNow;
        
        return Result.Success(this);
    }
    
    private bool IsPriceJumpTooBig(decimal newPrice) 
        => newPrice < CurrentPrice / 2 || newPrice > CurrentPrice * 1.5m;
}