using BettingGame.Tournament.Core.Features.GameOverview.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.GameOverview
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureGameOverview<TGameReader>(this IServiceCollection services)
            where TGameReader : class, IGameReader
        {
            // External dependency
            services.AddSingleton<IGameReader, TGameReader>();

            // QueryHandler
            services.AddScoped<ISubscriber, GamesQueryHandler>();

            return services;
        }
    }
}
