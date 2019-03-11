using BettingGame.Tournament.Core.Features.TeamAdministration.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.TeamAdministration
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureTeamAdministration<TTeamCommandRepository>(this IServiceCollection services)
            where TTeamCommandRepository : class, ITeamCommandRepository
        {
            // External dependency
            services.AddSingleton<ITeamCommandRepository, TTeamCommandRepository>();

            // Shared internal dependency

            // CommandHandler
            services.AddScoped<ISubscriber, UpdateTeamCommandHandler>();
            services.AddScoped<ISubscriber, DeleteTeamCommandHandler>();

            services.AddScoped<ISubscriber, CreateTeamCommandHandler>();

            // QueryHandler

            return services;
        }
    }
}
