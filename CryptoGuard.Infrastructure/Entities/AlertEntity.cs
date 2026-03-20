namespace CryptoGuard.Infrastructure.Entities;

public class AlertEntity
{
    public Guid Id { get; set; }
    public decimal TargetPrice { get; set; }
    public bool IsAscending { get; set; }
    public bool IsTriggered { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid AssertId { get; set; }
    public AssetEntity AssetEntity { get; set; } = null!;
}