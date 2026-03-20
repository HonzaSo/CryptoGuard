using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoGuard.Infrastructure.Entities.Configurations;

public class AlertConfiguration : IEntityTypeConfiguration<AlertEntity>
{
    public void Configure(EntityTypeBuilder<AlertEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.AssetEntity)
            .WithMany(asset => asset.Alerts)
            .HasForeignKey(a => a.AssertId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(a => a.TargetPrice).IsRequired().HasPrecision(18, 8);
        builder.Property(a => a.CreatedAt).IsRequired();
    }
    
}