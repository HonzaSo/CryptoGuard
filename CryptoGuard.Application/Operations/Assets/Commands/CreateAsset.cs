using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;

namespace CryptoGuard.Application.Operations.Assets.Commands;

public record CreateAssetCommand(string Symbol, string Name, string Currency, decimal CurrentPrice);

public class CreateAssetHandler(IAssetRepository assetRepository) : ICommandHandler<CreateAssetCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(CreateAssetCommand command, CancellationToken ct)
    {
        var asset = new Asset 
        { 
            Id = Guid.NewGuid(), 
            Symbol = command.Symbol, 
            Name = command.Name,
            Currency = command.Currency,
            CurrentPrice = command.CurrentPrice,
            LastUpdated = DateTime.UtcNow
        };

        var guid = await assetRepository.CreateAssetAsync(asset, ct);
        return Result.Success(guid);
    }
}