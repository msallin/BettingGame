using BettingGame.Betting.Core.Features.GameHandling.Abstraction;
using BettingGame.Betting.Core.Features.ParticipantBet.Abstraction;
using BettingGame.Betting.Core.Shared.Abstraction;
using BettingGame.Framework.Security;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.ParticipantBet
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureParticipantBet<TPrincipalProvider, TBetCommandRepository, TGameMetadataRepository, TBetReader>(this IServiceCollection services)
            where TPrincipalProvider : class, IPrincipalProvider
            where TBetCommandRepository : class, IBetCommandRepository
            where TBetReader : class, IBetReader
            where TGameMetadataRepository : class, IGameMetadataCommandRepository
        {
            // External dependency
            services.AddSingleton<IPrincipalProvider, TPrincipalProvider>();
            services.AddSingleton<IBetCommandRepository, TBetCommandRepository>();
            services.AddSingleton<IBetReader, TBetReader>();
            services.AddSingleton<IGameMetadataCommandRepository, TGameMetadataRepository>();

            // CommandHandler
            services.AddScoped<ISubscriber, ParticipantBetCommandHandler>();

            // QueryHandler
            services.AddScoped<ISubscriber, ParticipantBetQueryHandler>();

            return services;
        }
    }
}
