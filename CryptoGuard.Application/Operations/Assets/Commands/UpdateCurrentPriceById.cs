using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Application.Operations.Assets.Commands;

public record UpdateCurrentPriceByIdCommand(Guid Id, decimal CurrentPrice);

public class UpdateCurrentPriceByIdHandler(IAssetRepository assetRepository) : ICommandHandler<UpdateCurrentPriceByIdCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(UpdateCurrentPriceByIdCommand command, CancellationToken ct)
    {
        var asset = await assetRepository.GetAssetByIdAsync(command.Id, ct);
        if (asset == null)
        {
            return Result.Failure(new Error("AssetNotFound", $"No asset found with id '{command.Id}'"));
        }

        await assetRepository.UpdateCurrentPriceAsync(command.Id, command.CurrentPrice, ct);
        return Result.Success(Unit.Value);
    }
}
