namespace CryptoGuard.Domain.Domains;

public class Asset
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastUpdated { get; set; }
}