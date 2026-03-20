
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Domains;
using CryptoGuard.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoGuard.Infrastructure.Repositories;

public class AssetRepository (ApplicationDbContext context) : IAssetRepository
{
    public async Task<Guid> CreateAssetAsync(Asset asset, CancellationToken ct = default)
    {
        var assetEntity = new AssetEntity
        {
            Id = Guid.NewGuid(),
            Symbol = asset.Symbol,
            Name = asset.Name,
            Currency = asset.Currency,
            CurrentPrice = asset.CurrentPrice,
            LastUpdated = asset.LastUpdated
        };

        await context.Assets.AddAsync(assetEntity, ct);
        await context.SaveChangesAsync(ct);
        
        return assetEntity.Id;
    }

    public async Task<Asset?> GetAssetBySymbolAsync(string symbol, CancellationToken ct)
    {
        var assetEntity = await context.Assets.FirstOrDefaultAsync(a => a.Symbol == symbol, ct);
        if (assetEntity == null)
        {
            return null;
        }

        return new Asset
        {
            Id = assetEntity.Id,
            Symbol = assetEntity.Symbol,
            Name = assetEntity.Name,
            Currency = assetEntity.Currency,
            CurrentPrice = assetEntity.CurrentPrice,
            LastUpdated = assetEntity.LastUpdated
        };
    }
}
