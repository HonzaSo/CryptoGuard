using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Application.Operations.Assets.Queries;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;
using Microsoft.AspNetCore.Mvc;

namespace CryptoGuard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController (
    ICommandHandler<CreateAssetCommand, Result<Guid>> createHandler,
    ICommandHandler<GetAssetBySymbolQuery, Result<Asset>> getAssetBySymbolHandler
        ) : ControllerBase
{
    [HttpPost("add-asset")]
    public async Task<IActionResult> AddAsset([FromBody] CreateAssetCommand command, CancellationToken ct)
    {
        var result = await createHandler.HandleAsync(command, ct);
        
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetAssetBySymbol([FromRoute] string symbol, CancellationToken ct)
    {
        var result = await getAssetBySymbolHandler.HandleAsync(new GetAssetBySymbolQuery(symbol), ct);

        return result.IsSuccess 
            ? Ok(result.Value) 
            : NotFound(result.Error);
    }
}