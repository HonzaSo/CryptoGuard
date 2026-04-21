using CryptoGuard.Domain.Domains;
using FluentAssertions;

namespace CryptoGuard.Test.UnitTests.Domain.Domains;

public class CurrencyTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void Create_ShouldReturnSuccess_WhenCurrencyCodeIsValid()
        {
            var currencyCode = "USD";

            var result = Currency.Create(currencyCode);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Code.Should().Be("USD");
        }

        [Test]
        public void Create_ShouldNormalizeToUppercase_WhenCurrencyIsLowercase()
        {
            var currencyCode = "usd";

            var result = Currency.Create(currencyCode);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Code.Should().Be("USD");
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Create_ShouldReturnFailure_WhenCurrencyIsNullOrEmpty(string invalidCurrency)
        {
            var result = Currency.Create(invalidCurrency);

            result.IsSuccess.Should().BeFalse();
            result.Error!.Code.Should().Be("Currency.Invalid");
            result.Error.Description.Should().Be("Měna musí mít přesně 3 znaky.");
        }

        [TestCase("A")]
        [TestCase("AB")]
        [TestCase("ABCD")]
        [TestCase("ABCDE")]
        public void Create_ShouldReturnFailure_WhenCurrencyLengthIsNotExactly3(string invalidCurrency)
        {
            var result = Currency.Create(invalidCurrency);

            result.IsSuccess.Should().BeFalse();
            result.Error!.Code.Should().Be("Currency.Invalid");
            result.Error.Description.Should().Be("Měna musí mít přesně 3 znaky.");
        }

        [TestCase("USD")]
        [TestCase("EUR")]
        [TestCase("CZK")]
        [TestCase("GBP")]
        [TestCase("JPY")]
        public void Create_ShouldReturnSuccess_WhenCurrencyIsExactly3Characters(string validCurrency)
        {
            var result = Currency.Create(validCurrency);

            result.IsSuccess.Should().BeTrue();
            result.Value!.Code.Should().Be(validCurrency);
        }
    }
}
