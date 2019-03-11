using BettingGame.Betting.Core.Features.TeamHandling.Abstraction;
using BettingGame.Betting.Core.Shared.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.TeamHandling
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureTeamHandling<TBetCommandRepository, TTeamMetadataCommandRepository>(this IServiceCollection services)
            where TBetCommandRepository : class, IBetCommandRepository
            where TTeamMetadataCommandRepository : class, ITeamMetadataCommandRepository
        {
            // External dependency
            services.AddSingleton<IBetCommandRepository, TBetCommandRepository>();
            services.AddSingleton<ITeamMetadataCommandRepository, TTeamMetadataCommandRepository>();

            // Shared internal dependency

            // CommandHandler
            services.AddScoped<ISubscriber, TeamChangedEventHandler>();

            // QueryHandler

            return services;
        }
    }
}
