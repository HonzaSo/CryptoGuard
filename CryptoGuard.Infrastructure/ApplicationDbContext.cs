using CryptoGuard.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoGuard.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
    }
    
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Alert> Alerts => Set<Alert>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}