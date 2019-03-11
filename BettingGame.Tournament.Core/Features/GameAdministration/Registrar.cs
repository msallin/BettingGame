using BettingGame.Tournament.Core.Features.GameAdministration.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureGameAdministration<TGameCommandRepository>(this IServiceCollection services)
            where TGameCommandRepository : class, IGameCommandRepository
        {
            // External dependency
            services.AddSingleton<IGameCommandRepository, TGameCommandRepository>();

            // Shared internal dependency

            // CommandHandler
            services.AddScoped<ISubscriber, CreateGameCommandHandler>();
            services.AddScoped<ISubscriber, UpdateGameCommandHandler>();
            services.AddScoped<ISubscriber, DeleteGameCommandHandler>();

            // QueryHandler

            return services;
        }
    }
}
