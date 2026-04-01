using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Application.Operations.Assets.Commands;

public record RemoveAssetCommand(Guid Id);

public class RemoveAssetHandler(IAssetRepository assetRepository) : ICommandHandler<RemoveAssetCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(RemoveAssetCommand command, CancellationToken ct)
    {
        var asset = await assetRepository.GetAssetByIdAsync(command.Id, ct);
        if (asset == null)
        {
            return Result.Failure(new Error("AssetNotFound", $"No asset found with id '{command.Id}'"));
        }

        await assetRepository.RemoveAssetAsync(command.Id, ct);
        return Result.Success(Unit.Value);
    }
}
