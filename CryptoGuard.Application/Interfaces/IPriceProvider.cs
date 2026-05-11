using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Application.Interfaces;

public interface IPriceProvider
{
    Task<Result<decimal>> GetPriceAsync(string coinId, string targetCurrency, CancellationToken ct);
}