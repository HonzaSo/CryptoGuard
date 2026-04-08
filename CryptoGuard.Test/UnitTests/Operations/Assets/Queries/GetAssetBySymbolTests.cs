using CryptoGuard.Application.Interfaces;
using CryptoGuard.Application.Operations.Assets.Queries;
using CryptoGuard.Domain.Domains;
using FluentAssertions;
using NSubstitute;

namespace CryptoGuard.Test.UnitTests.Operations.Assets.Queries;

public class GetAssetBySymbolTests
{
    private readonly IAssetRepository _assetRepository;
    private readonly GetAssetBySymbolHandler _handler;

    public GetAssetBySymbolTests()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _handler = new GetAssetBySymbolHandler(_assetRepository);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAsset_WhenAssetExists()
    {
        var assetId = Guid.NewGuid();
        var asset = new Asset
        {
            Id = assetId,
            Symbol = "BTC",
            Name = "Bitcoin",
            Currency = "USD",
            CurrentPrice = 50000m,
            LastUpdated = DateTime.UtcNow
        };

        _assetRepository.GetAssetBySymbolAsync("BTC", Arg.Any<CancellationToken>())
            .Returns(asset);
        var query = new GetAssetBySymbolQuery("BTC");

        var result = await _handler.HandleAsync(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(asset);
    }
}

