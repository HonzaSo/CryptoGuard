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
            Symbol = asset.Symbol.ToUpper(),
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
        var assetEntity = await context.Assets.FirstOrDefaultAsync(a => a.Symbol == symbol.ToUpper(), ct);
        if (assetEntity == null)
        {
            return null;
        }

        return new Asset(
            assetEntity.Id,
            assetEntity.Symbol,
            assetEntity.Name,
            assetEntity.Currency,
            assetEntity.CurrentPrice,
            assetEntity.LastUpdated
        );
    }

    public async Task<Asset?> GetAssetByIdAsync(Guid id, CancellationToken ct)
    {
        var assetEntity = await context.Assets.FirstOrDefaultAsync(a => a.Id == id, ct);
        if (assetEntity == null)
        {
            return null;
        }

        return new Asset(
            assetEntity.Id,
            assetEntity.Symbol,
            assetEntity.Name,
            assetEntity.Currency,
            assetEntity.CurrentPrice,
            assetEntity.LastUpdated
        );
    }

    public async Task RemoveAssetAsync(Guid id, CancellationToken ct)
    {
        var assetEntity = await context.Assets.FirstOrDefaultAsync(a => a.Id == id, ct);
        if (assetEntity != null)
        {
            context.Assets.Remove(assetEntity);
            await context.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateCurrentPriceAsync(Guid id, decimal newPrice, CancellationToken ct)
    {
        var assetEntity = await context.Assets.FirstOrDefaultAsync(a => a.Id == id, ct);
        if (assetEntity != null)
        {
            assetEntity.CurrentPrice = newPrice;
            assetEntity.LastUpdated = DateTime.UtcNow;
            await context.SaveChangesAsync(ct);
        }
    }

    public async Task UpdateAssetAsync(Asset asset, CancellationToken ct)
    {
        var existingEntity = await context.Assets
        .FirstOrDefaultAsync(a => a.Id == asset.Id, ct);

        if (existingEntity == null)
        {
            return;
        }

        existingEntity.Symbol = asset.Symbol;
        existingEntity.Name = asset.Name;
        existingEntity.Currency = asset.Currency;
        existingEntity.CurrentPrice = asset.CurrentPrice;
        existingEntity.LastUpdated = asset.LastUpdated;

        await context.SaveChangesAsync(ct);
    }
}
