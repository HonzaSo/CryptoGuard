using CryptoGuard.Application.Common.Interfaces;
using CryptoGuard.Application.Operations.Assets.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoGuard.Application;

public static class DependencyInjection
{
    public static void AddApplicationDi(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateAssetCommand, Guid>, CreateAssetHandler>();
    }
}