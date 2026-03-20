using CryptoGuard.Domain.Domains;

namespace CryptoGuard.Application.Interfaces;

public interface IAssetRepository
{
    Task<Guid> CreateAssetAsync(Asset asset, CancellationToken ct);
}