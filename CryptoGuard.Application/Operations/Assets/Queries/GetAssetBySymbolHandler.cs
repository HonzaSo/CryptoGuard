using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Domains;

namespace CryptoGuard.Application.Operations.Assets.Queries;

public record GetAssetBySymbolQuery(string Symbol);

public class GetAssetBySymbolHandler (IAssetRepository assetRepository) : ICommandHandler<GetAssetBySymbolQuery, Asset?>
{
    public async Task<Asset?> HandleAsync(GetAssetBySymbolQuery command, CancellationToken ct)
    {
        var result = await assetRepository.GetAssetBySymbolAsync(command.Symbol, ct);
        return result;
    }
}