namespace CryptoGuard.Infrastructure.Entities;

public class Alert
{
    public Guid Id { get; set; }
    public decimal TargetPrice { get; set; }
    public bool IsAscending { get; set; }
    public bool IsTriggered { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid AssertId { get; set; }
    public Asset Asset { get; set; } = null!;
}