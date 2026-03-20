using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Application.Operations.Assets.Queries;
using CryptoGuard.Domain.Domains;
using Microsoft.AspNetCore.Mvc;

namespace CryptoGuard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController (
    ICommandHandler<CreateAssetCommand, Guid> createHandler,
    ICommandHandler<GetAssetBySymbolQuery, Asset?> getAssetBySymbolHandler
        ) : ControllerBase
{
    [HttpPost("add-asset")]
    public async Task<IActionResult> AddAsset([FromBody] CreateAssetCommand command, CancellationToken ct)
    {
        var result = await createHandler.HandleAsync(command, ct);
        return Ok(result);
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetAssetBySymbol([FromRoute] string symbol, CancellationToken ct)
    {
        var result = await getAssetBySymbolHandler.HandleAsync(new GetAssetBySymbolQuery(symbol), ct);
        return result != null ? Ok(result) : NotFound("Symbol not found");
    }
}