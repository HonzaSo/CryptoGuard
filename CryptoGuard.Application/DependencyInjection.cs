using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using CryptoGuard.Application.Operations.Assets.Queries;
using CryptoGuard.Domain.Abstractions;
using CryptoGuard.Domain.Domains;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoGuard.Application;

public static class DependencyInjection
{
    public static void AddApplicationDi(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateAssetCommand, Result<Guid>>, CreateAssetHandler>();
        services.AddScoped<ICommandHandler<GetAssetBySymbolQuery, Result<Asset>>, GetAssetBySymbolHandler>();
    }
}