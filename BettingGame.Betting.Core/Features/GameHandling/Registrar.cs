using BettingGame.Betting.Core.Features.GameHandling.Abstraction;
using BettingGame.Betting.Core.Shared.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.GameHandling
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureGameHandling<TBetCommandRepository, TGameMetadataCommandRepository>(this IServiceCollection services)
            where TBetCommandRepository : class, IBetCommandRepository
            where TGameMetadataCommandRepository : class, IGameMetadataCommandRepository
        {
            // External dependency
            services.AddSingleton<IBetCommandRepository, TBetCommandRepository>();
            services.AddSingleton<IGameMetadataCommandRepository, TGameMetadataCommandRepository>();

            // Shared internal dependency

            // CommandHandler
            services.AddScoped<ISubscriber, SetActualResultCommandHandler>();
            services.AddScoped<ISubscriber, SaveGameMetadataCommandHandler>();

            // QueryHandler

            return services;
        }
    }
}
