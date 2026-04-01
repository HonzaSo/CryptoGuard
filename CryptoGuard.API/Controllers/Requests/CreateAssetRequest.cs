namespace CryptoGuard.API.Controllers.Requests;

public record CreateAssetRequest(string Symbol, string Name, string Currency, decimal CurrentPrice);

