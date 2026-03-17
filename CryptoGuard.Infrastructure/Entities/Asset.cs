namespace CryptoGuard.Infrastructure.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastUpdated { get; set; }
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}