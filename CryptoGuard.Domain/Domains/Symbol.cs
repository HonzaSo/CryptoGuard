using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Domain.Domains;

public record Symbol
{
    public string Value { get; init; }
    
    private Symbol(string value) => Value = value;
    
    public static Result<Symbol> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure(new Error("Symbol.Invalid", "Symbol cannot be empty."));
        }
        
        if (value.Length != 3)
        {
            return Result.Failure(new Error("Symbol.Invalid", "Symbol must be exactly 3 characters long."));
        }

        return Result.Success(new Symbol(value.ToUpper()));
    }
}