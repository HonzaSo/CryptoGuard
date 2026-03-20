using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CryptoGuard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController (ICommandHandler<CreateAssetCommand, Guid> handler) : ControllerBase
{
    [HttpPost("add-asset")]
    public async Task<IActionResult> AddAsset([FromBody] CreateAssetCommand command, CancellationToken ct)
    {
        var result = await handler.HandleAsync(command, ct);
        return Ok(result);
    }
}