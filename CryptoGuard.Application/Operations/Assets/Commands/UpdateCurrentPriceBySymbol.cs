using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Application.Operations.Assets.Commands;

public record UpdateCurrentPriceBySymbolCommand(string Symbol, decimal CurrentPrice);

public class UpdateCurrentPriceBySymbolHandler(IAssetRepository assetRepository) : ICommandHandler<UpdateCurrentPriceBySymbolCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(UpdateCurrentPriceBySymbolCommand command, CancellationToken ct)
    {
        var asset = await assetRepository.GetAssetBySymbolAsync(command.Symbol, ct);
        if (asset == null)
        {
            return Result.Failure(new Error("AssetNotFound", $"No asset found with symbol '{command.Symbol}'"));
        }

        await assetRepository.UpdateCurrentPriceAsync(asset.Id, command.CurrentPrice, ct);
        return Result.Success(Unit.Value);
    }
}
