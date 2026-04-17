using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;

namespace CryptoGuard.Application.Operations.Assets.Commands;

public record UpdateCurrentPriceByIdCommand(Guid Id, decimal CurrentPrice);

public class UpdateCurrentPriceByIdHandler(IAssetRepository assetRepository) : ICommandHandler<UpdateCurrentPriceByIdCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(UpdateCurrentPriceByIdCommand command, CancellationToken ct)
    {
        var asset = await assetRepository.GetAssetByIdAsync(command.Id, ct);
        if (asset == null)
        {
            return Result.Failure(AssetErrors.AssetNotFound);
        }
        
        var result = asset.UpdateCurrentPrice(command.CurrentPrice);
        
        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error!);
        }
        
        await assetRepository.UpdateAssetAsync(asset, ct);
        return Result.Success(Unit.Value);
    }
}
