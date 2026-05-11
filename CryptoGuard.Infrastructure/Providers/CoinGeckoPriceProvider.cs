using System.Net.Http.Json;
using CryptoGuard.Application.Interfaces;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace CryptoGuard.Infrastructure.Providers;

public class CoinGeckoPriceProvider(HttpClient httpClient, 
    IOptions<CoinGeckoOptions> options) : IPriceProvider
{
    private readonly CoinGeckoOptions _options = options.Value;

    public async Task<Result<decimal>> GetPriceAsync(string coinId, string targetCurrency, CancellationToken ct)
    {
        try
        {
            var url = $"simple/price?ids={coinId.ToLower()}&vs_currencies={targetCurrency.ToLower()}";
            // base address and timeout jsou nastaveny v DependencyInjection, takže zde pouze voláme endpoint s parametry
            var response = await httpClient.GetAsync(url, ct);

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure(new Error("ExternalApi.Error", "Chyba komunikace s API."));
            }

            var data = await response.Content.ReadFromJsonAsync<Dictionary<string, Dictionary<string, decimal>>>(cancellationToken: ct);

            if (data != null && 
                data.TryGetValue(coinId.ToLower(), out var currencyMap) && 
                currencyMap.TryGetValue(targetCurrency.ToLower(), out var price))
            {
                return Result.Success(price);
            }

            return Result.Failure(new Error("Price.NotFound", "Cena nebyla v odpovědi nalezena."));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ExternalApi.Exception", ex.Message));
        }
    }
}