using CryptoGuard.Application.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;
using FluentAssertions;
using NSubstitute;

namespace CryptoGuard.Test.UnitTests.Operations.Assets.Commands;

public class RemoveAssetTests
{
    private readonly IAssetRepository _assetRepository;
    private readonly RemoveAssetHandler _handler;

    public RemoveAssetTests()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _handler = new RemoveAssetHandler(_assetRepository);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnSuccess_WhenAssetExists()
    {
        var assetId = Guid.NewGuid();
        var asset = new Asset(
            assetId,
             "BTC",
             "Bitcoin",
             Currency.Usd,
             50000m,
             DateTime.UtcNow);   
        
        _assetRepository.GetAssetByIdAsync(assetId, Arg.Any<CancellationToken>())
            .Returns(asset);
        var command = new RemoveAssetCommand(assetId);

        var result = await _handler.HandleAsync(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(Unit.Value);
        await _assetRepository.Received(1).RemoveAssetAsync(assetId, Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task HandleAsync_ShouldNotRemoveAssetAsync_WhenAssetDoesNotExist()
    {
        var assetId = Guid.NewGuid();
        _assetRepository.GetAssetByIdAsync(assetId, Arg.Any<CancellationToken>())
            .Returns((Asset?)null);
        var command = new RemoveAssetCommand(assetId);

        var result = await _handler.HandleAsync(command, CancellationToken.None);

        result.Error?.Code.Should().Be("AssetNotFound");
        await _assetRepository.DidNotReceive().RemoveAssetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
}

