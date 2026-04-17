using CryptoGuard.Domain.Domains;

namespace CryptoGuard.Application.Interfaces;

public interface IAssetRepository
{
    Task<Guid> CreateAssetAsync(Asset asset, CancellationToken ct);
    Task<Asset?> GetAssetBySymbolAsync(Symbol symbol, CancellationToken ct);
    Task<Asset?> GetAssetByIdAsync(Guid id, CancellationToken ct);
    Task RemoveAssetAsync(Guid id, CancellationToken ct);
    Task UpdateCurrentPriceAsync(Guid id, decimal newPrice, CancellationToken ct);
    Task UpdateAssetAsync(Asset asset, CancellationToken ct);
}