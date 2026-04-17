using CryptoGuard.Domain.Domains;

namespace CryptoGuard.Infrastructure.Entities;

public class AssetEntity
{
    public Guid Id { get; set; }
    public Symbol Symbol { get; set; }
    public string Name { get; set; }
    public Currency Currency { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastUpdated { get; set; }
    public ICollection<AlertEntity> Alerts { get; set; } = new List<AlertEntity>();
}