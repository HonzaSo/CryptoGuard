using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;


namespace CryptoGuard.Application.Operations.Assets.Queries;

public record GetAssetBySymbolQuery(string Symbol);

public class GetAssetBySymbolHandler (IAssetRepository assetRepository) : ICommandHandler<GetAssetBySymbolQuery, Result<Asset>>
{
    public async Task<Result<Asset>> HandleAsync(GetAssetBySymbolQuery command, CancellationToken ct)
    {
        var result = await assetRepository.GetAssetBySymbolAsync(command.Symbol, ct);
        
        if (result == null)
        {
            return Result.Failure(new Error("AssetNotFound", $"No asset found with symbol '{command.Symbol}'"));
        }
        
        return Result.Success(result);
    }
}