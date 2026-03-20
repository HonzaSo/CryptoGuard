using CryptoGuard.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoGuard.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
    }
    
    public DbSet<AssetEntity> Assets => Set<AssetEntity>();
    public DbSet<AlertEntity> Alerts => Set<AlertEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}