using CryptoGuard.Application.Interfaces;
using CryptoGuard.Infrastructure.Configurations;
using CryptoGuard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoGuard.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureDi(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = configuration.GetSection("Database").Get<Database>() ?? throw new InvalidOperationException("Failed to load database configuration.");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dbConfiguration.ConnectionString));
        
        services.AddScoped<IAssetRepository, AssetRepository>();
    }
}