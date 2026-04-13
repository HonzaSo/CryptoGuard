using CryptoGuard.Application.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;
using FluentAssertions;
using NSubstitute;

namespace CryptoGuard.Test.UnitTests.Operations.Assets.Commands;

public class UpdateCurrentPriceBySymbolTests
{
    private readonly IAssetRepository _assetRepository;
    private readonly UpdateCurrentPriceBySymbolHandler _handler;

    public UpdateCurrentPriceBySymbolTests()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _handler = new UpdateCurrentPriceBySymbolHandler(_assetRepository);
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

        _assetRepository.GetAssetBySymbolAsync("BTC", Arg.Any<CancellationToken>())
            .Returns(asset);
        var command = new UpdateCurrentPriceBySymbolCommand("BTC", 55000m);

        var result = await _handler.HandleAsync(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(Unit.Value);
        await _assetRepository.Received(1).UpdateCurrentPriceAsync(assetId, 55000m, Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task HandleAsync_ShouldReturnFailure_WhenAssetDoesNotExist()
    {
        var command = new UpdateCurrentPriceBySymbolCommand("XYZ", 55000m);
        _assetRepository.GetAssetBySymbolAsync("XYZ", Arg.Any<CancellationToken>())
            .Returns((Asset?)null);

        var result = await _handler.HandleAsync(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error?.Code.Should().Be("AssetNotFound");
        await _assetRepository.DidNotReceive().UpdateCurrentPriceAsync(Arg.Any<Guid>(), Arg.Any<decimal>(), Arg.Any<CancellationToken>());
    }
}

