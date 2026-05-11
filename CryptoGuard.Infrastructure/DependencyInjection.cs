using CryptoGuard.Application.Interfaces;
using CryptoGuard.Infrastructure.BackgroundJobs;
using CryptoGuard.Infrastructure.Configurations;
using CryptoGuard.Infrastructure.Providers;
using CryptoGuard.Infrastructure.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CryptoGuard.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureDi(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = configuration.GetSection("Database").Get<Database>() ?? throw new InvalidOperationException("Failed to load database configuration.");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dbConfiguration.ConnectionString));
        
        services.AddHangfire(config => config.UsePostgreSqlStorage(dbConfiguration.ConnectionString));
        services.AddHangfireServer();
        
        services.AddScoped<IAssetRepository, AssetRepository>();
        
        var coingeckoSection = configuration.GetSection("CoinGeckoOptions");
        var coingeckoOptions = coingeckoSection.Get<CoinGeckoOptions>() 
                               ?? throw new InvalidOperationException("CoinGeckoOptions section is missing.");

        services.AddSingleton(Options.Create(coingeckoOptions));
        services.AddScoped<UpdatePricesJob>();

        services.AddHttpClient<IPriceProvider, CoinGeckoPriceProvider>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<CoinGeckoOptions>>().Value;
        
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
        
            client.DefaultRequestHeaders.Add("User-Agent", "CryptoGuardApp/1.0");
            
            if (!string.IsNullOrEmpty(options.ApiKey))
            {
                client.DefaultRequestHeaders.Add("x-cg-demo-api-key", options.ApiKey);
            }
        });
    }
}