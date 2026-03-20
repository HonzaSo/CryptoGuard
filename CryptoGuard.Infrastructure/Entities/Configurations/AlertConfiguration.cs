using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoGuard.Infrastructure.Entities.Configurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.Asset)
            .WithMany(asset => asset.Alerts)
            .HasForeignKey(a => a.AssertId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(a => a.TargetPrice).IsRequired().HasPrecision(18, 8);
        builder.Property(a => a.CreatedAt).IsRequired();
    }
    
}