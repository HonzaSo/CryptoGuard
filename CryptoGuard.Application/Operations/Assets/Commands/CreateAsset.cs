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
        var symbolResult = Symbol.Create(command.Symbol);
        var currencyResult = Currency.Create(command.Currency);
    
        if (!currencyResult.IsSuccess)
        {
            return Result.Failure(currencyResult.Error);
        }
        
        if (!symbolResult.IsSuccess)
        {
            return Result.Failure(symbolResult.Error);
        }
        
        var asset = new Asset(
            Guid.NewGuid(),
            symbolResult.Value,
            command.Name,
            currencyResult.Value,
            command.CurrentPrice,
            DateTime.UtcNow
        );

        var guid = await assetRepository.CreateAssetAsync(asset, ct);
        return Result.Success(guid);
    }
}