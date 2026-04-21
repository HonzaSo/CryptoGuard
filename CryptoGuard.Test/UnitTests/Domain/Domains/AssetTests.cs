using CryptoGuard.Domain.Domains;
using FluentAssertions;

namespace CryptoGuard.Test.UnitTests.Domain.Domains;

[TestFixture]
public class AssetTests
{
    [Test]
    public void UpdateCurrentPrice_ShouldReturnFailure_WhenNewPriceIsNegative()
    {
        var asset = new Asset(
            Guid.NewGuid(),
            Symbol.Btc,
            "Bitcoin",
            Currency.Usd,
            50000m,
            DateTime.UtcNow);
        var newPrice = -100m;

        var result = asset.UpdateCurrentPrice(newPrice);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("Asset.NegativePrice");
        result.Error.Description.Should().Be("Cena aktiva nesmí být záporná.");
        asset.CurrentPrice.Should().Be(50000m);
    }
    
    [TestCase(10.0)]
    [TestCase(151.0)]
    public void UpdateCurrentPrice_ShouldReturnFailure_WhenPriceJumpIsTooBig_MoreThan50Percent(decimal newPrice)
    {
        var asset = new Asset(
            Guid.NewGuid(),
            Symbol.Btc,
            "Bitcoin",
            Currency.Usd,
            100m,
            DateTime.UtcNow);

        var result = asset.UpdateCurrentPrice(newPrice);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("Asset.PriceJumpTooBig");
        result.Error.Description.Should().Be("Změna ceny je příliš prudká (o více než 50 %).");
    }

    [Test]
    public void UpdateCurrentPrice_ShouldReturnFailure_WhenNewPriceIsSameAsCurrent()
    {
        var asset = new Asset(
            Guid.NewGuid(),
            Symbol.Btc,
            "Bitcoin",
            Currency.Usd,
            50000m,
            DateTime.UtcNow);
        var newPrice = 50000m;

        var result = asset.UpdateCurrentPrice(newPrice);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("Asset.SamePrice");
        result.Error.Description.Should().Be("Nová cena je totožná s aktuální cenou.");
    }
}