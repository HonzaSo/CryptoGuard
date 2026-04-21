using CryptoGuard.Application.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CryptoGuard.Test.UnitTests.Operations.Assets.Commands;

public class UpdateCurrentPriceByIdTests
{
    private readonly IAssetRepository _assetRepository;
    private readonly UpdateCurrentPriceByIdHandler _handler;

    public UpdateCurrentPriceByIdTests()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _handler = new UpdateCurrentPriceByIdHandler(_assetRepository);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnSuccess_WhenAssetExists()
    {
        var assetId = Guid.NewGuid();
        var asset = new Asset(
            assetId,
            Symbol.Create("BTC").Value!,
            "Bitcoin",
            Currency.Usd,
            50000m,
            DateTime.UtcNow);   


        _assetRepository.GetAssetByIdAsync(assetId, Arg.Any<CancellationToken>())
            .Returns(asset);
        var command = new UpdateCurrentPriceByIdCommand(assetId, 55000m);

        var result = await _handler.HandleAsync(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(Unit.Value);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnFailure_WhenAssetDoesNotExist()
    {
        var assetId = Guid.NewGuid();
        _assetRepository.GetAssetByIdAsync(assetId, Arg.Any<CancellationToken>())
            .ReturnsNull();
        var command = new UpdateCurrentPriceByIdCommand(assetId, 55000m);

        var result = await _handler.HandleAsync(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error?.Code.Should().Be("Asset.NotFound");
    }
}

