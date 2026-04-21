using CryptoGuard.Domain.Domains;
using FluentAssertions;

namespace CryptoGuard.Test.UnitTests.Domain.Domains;

public class SymbolTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void Create_ShouldReturnSuccess_WhenSymbolIsValid()
        {
            var symbolCode = "BTC";

            var result = Symbol.Create(symbolCode);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Value.Should().Be("BTC");
        }

        [Test]
        public void Create_ShouldNormalizeToUppercase_WhenSymbolIsLowercase()
        {
            var symbolCode = "btc";

            var result = Symbol.Create(symbolCode);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Value.Should().Be("BTC");
        }

        [Test]
        public void Create_ShouldTrimWhitespace_WhenSymbolHasLeadingOrTrailingSpaces()
        {
            var symbolCode = "  ETH  ";

            var result = Symbol.Create(symbolCode);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Value.Should().Be("ETH");
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Create_ShouldReturnFailure_WhenSymbolIsNullOrEmpty(string invalidSymbol)
        {
            var result = Symbol.Create(invalidSymbol);

            result.IsSuccess.Should().BeFalse();
            result.Error!.Code.Should().Be("Symbol.Invalid");
            result.Error!.Description.Should().Be("Symbol nemůže být prázdný.");
        }

        [TestCase("BT")]
        [TestCase("ETHH")]
        public void Create_ShouldReturnFailure_WhenSymbolIsNotExactly3Characters(string invalidSymbol)
        {
            var result = Symbol.Create(invalidSymbol);

            result.IsSuccess.Should().BeFalse();
            result.Error!.Code.Should().Be("Symbol.Invalid");
            result.Error.Description.Should().Be("Symbol musí mít přesně 3 charaktery.");
        }
    }
}
