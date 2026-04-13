using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Domain.Domains;

public record Currency
{
    public string Code { get; init; }
    
    private Currency(string code) => Code = code;
    
    public static Result<Currency> Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
        {
            return Result.Failure(new Error("Currency.Invalid", "Měna musí mít přesně 3 znaky."));
        }

        return Result.Success(new Currency(code.ToUpper()));
    }
    
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Czk = new("CZK");
}