using CryptoGuard.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoGuard.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureDi(this IServiceCollection service, IConfiguration configuration)
    {
        var dbConfiguration = configuration.GetSection("Database").Get<Database>() ?? throw new InvalidOperationException("Failed to load database configuration.");
        
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dbConfiguration.ConnectionString));
    }
}