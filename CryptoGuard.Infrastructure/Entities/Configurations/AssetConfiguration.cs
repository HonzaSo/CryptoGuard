using CryptoGuard.Domain.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoGuard.Infrastructure.Entities.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<AssetEntity>
{
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Symbol).IsRequired().HasMaxLength(10);
        builder.Property(a => a.Name).HasMaxLength(100);
        builder.Property(a => a.CurrentPrice).HasColumnType("decimal(18,8)");
        builder.Property(a => a.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .HasConversion(
                c => c.Code, 
                s => Currency.Create(s).Value!
            );
        
        builder.HasIndex(a => a.Symbol).IsUnique();
    }
}