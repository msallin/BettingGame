using BettingGame.Betting.Core.Features.ParticipantBet.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.ExportBetResults
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureExportBetResults<TBetReader>(this IServiceCollection services)
            where TBetReader : class, IBetReader
        {
            // External dependency
            services.AddSingleton<IBetReader, TBetReader>();

            // Shared internal dependency

            // CommandHandler

            // QueryHandler
            services.AddScoped<ISubscriber, BetsWithResultQueryHandler>();

            return services;
        }
    }
}
