using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Domain.Domains;

public record Symbol
{
    public string Value { get; init; }
    
    private Symbol(string value) => Value = value;
    
    public static Result<Symbol> Create(string value)
    {
        value = value.Trim();
        
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure(new Error("Symbol.Invalid", "Symbol nemůže být prázdný."));
        }
        
        if (value.Length != 3)
        {
            return Result.Failure(new Error("Symbol.Invalid", "Symbol musí mít přesně 3 charaktery."));
        }

        return Result.Success(new Symbol(value.ToUpper()));
    }
    
    public static readonly Symbol Btc = new("BTC");
    public static readonly Symbol Eth = new("ETH");
}