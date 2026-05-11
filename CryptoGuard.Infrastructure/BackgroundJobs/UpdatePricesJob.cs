using CryptoGuard.Application.Interfaces;

namespace CryptoGuard.Infrastructure.BackgroundJobs;

public class UpdatePricesJob(
    IPriceProvider priceProvider, 
    IAssetRepository assetRepository, 
    ApplicationDbContext context)
{
    public async Task ExecuteAsync()
    {
        var assets = await assetRepository.GetAssetsAsync(CancellationToken.None);

        foreach (var asset in assets)
        {
            var result = await priceProvider.GetPriceAsync(
                asset.Name, 
                asset.Currency.Code, 
                CancellationToken.None);

            if (result.IsSuccess)
            {
                asset.UpdateCurrentPrice(result.Value);
                
                await assetRepository.UpdateAssetAsync(asset, CancellationToken.None);
            }
        }

        await context.SaveChangesAsync(CancellationToken.None);
    }
}