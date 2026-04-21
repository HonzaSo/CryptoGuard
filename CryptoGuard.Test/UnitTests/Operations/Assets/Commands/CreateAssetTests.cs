using CryptoGuard.Application.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Domain.Domains;
using FluentAssertions;
using NSubstitute;

namespace CryptoGuard.Test.UnitTests.Operations.Assets.Commands;


public class CreateAssetTests
{
    private readonly IAssetRepository _assetRepository;
    private readonly CreateAssetHandler _handler;

    public CreateAssetTests()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _handler = new CreateAssetHandler(_assetRepository);
    }
    
    [Test]
    public async Task HandleAsync_ShouldReturnGuid_WhenCommandIsValid()
    {
        var assetId = Guid.NewGuid();
        _assetRepository.CreateAssetAsync(Arg.Any<Asset>(), Arg.Any<CancellationToken>())
            .Returns(assetId);
        var command = new CreateAssetCommand("BTC", "Bitcoin", "USD", 50000m);
        
        var result = await _handler.HandleAsync(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(assetId);
        await _assetRepository.Received(1)
            .CreateAssetAsync(Arg.Any<Asset>(), Arg.Any<CancellationToken>());
    }
}