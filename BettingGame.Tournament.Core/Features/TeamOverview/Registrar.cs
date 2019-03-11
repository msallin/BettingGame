using BettingGame.Tournament.Core.Features.TeamOverview.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.TeamOverview
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureTeamOverview<TTeamReader>(this IServiceCollection services)
            where TTeamReader : class, ITeamReader
        {
            // External dependency
            services.AddSingleton<ITeamReader, TTeamReader>();

            // QueryHandler
            services.AddScoped<ISubscriber, AllTeamsQueryHandler>();

            return services;
        }
    }
}
