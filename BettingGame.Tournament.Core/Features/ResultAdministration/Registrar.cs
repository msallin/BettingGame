using BettingGame.Tournament.Core.Features.ResultAdministration.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.ResultAdministration
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureResultAdministration<TGameResultCommandRepository>(this IServiceCollection services)
            where TGameResultCommandRepository : class, IGameResultCommandRepository
        {
            // External dependency
            services.AddSingleton<IGameResultCommandRepository, TGameResultCommandRepository>();

            // Shared internal dependency

            // CommandHandler
            services.AddScoped<ISubscriber, SetGameResultCommandHandler>();

            // QueryHandler

            return services;
        }
    }
}
