using BettingGame.Framework.Abstraction.Clients.Betting;
using BettingGame.Framework.Security;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.NotifyBettingAboutGame
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureNotifyBettingAboutGameChange<TBettingClient, TPrincipalProvider>(this IServiceCollection services)
            where TBettingClient : class, IBettingClient
            where TPrincipalProvider : class, IPrincipalProvider
        {
            // External dependency
            services.AddSingleton<IBettingClient, TBettingClient>();
            services.AddSingleton<IPrincipalProvider, TPrincipalProvider>(); // Used by the IBettingClient

            // Shared internal dependency

            // CommandHandler
            services.AddScoped<ISubscriber, GameChangedEventHandler>();

            // QueryHandler

            return services;
        }
    }
}
