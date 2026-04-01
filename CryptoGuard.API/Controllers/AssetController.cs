using CryptoGuard.API.Controllers.Requests;
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
    ICommandHandler<GetAssetBySymbolQuery, Result<Asset>> getAssetBySymbolHandler,
    ICommandHandler<RemoveAssetCommand, Result<Unit>> removeHandler,
    ICommandHandler<UpdateCurrentPriceByIdCommand, Result<Unit>> updatePriceByIdHandler,
    ICommandHandler<UpdateCurrentPriceBySymbolCommand, Result<Unit>> updatePriceBySymbolHandler
        ) : ControllerBase
{
    [HttpPost("add-asset")]
    public async Task<IActionResult> AddAsset([FromBody] CreateAssetRequest request, CancellationToken ct)
    {
        var command = new CreateAssetCommand(request.Symbol, request.Name, request.Currency, request.CurrentPrice);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsset([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await removeHandler.HandleAsync(new RemoveAssetCommand(id), ct);

        return result.IsSuccess 
            ? NoContent() 
            : NotFound(result.Error);
    }

    [HttpPut("{id}/price")]
    public async Task<IActionResult> UpdateCurrentPriceById([FromRoute] Guid id, [FromBody] UpdateCurrentPriceByIdRequest request, CancellationToken ct)
    {
        var result = await updatePriceByIdHandler.HandleAsync(new UpdateCurrentPriceByIdCommand(id, request.CurrentPrice), ct);

        return result.IsSuccess 
            ? NoContent() 
            : NotFound(result.Error);
    }

    [HttpPut("symbol/{symbol}/price")]
    public async Task<IActionResult> UpdateCurrentPriceBySymbol([FromRoute] string symbol, [FromBody] UpdateCurrentPriceBySymbolRequest request, CancellationToken ct)
    {
        var result = await updatePriceBySymbolHandler.HandleAsync(new UpdateCurrentPriceBySymbolCommand(symbol, request.CurrentPrice), ct);

        return result.IsSuccess 
            ? NoContent() 
            : NotFound(result.Error);
    }
}