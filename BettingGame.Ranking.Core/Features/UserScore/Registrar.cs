using BettingGame.Ranking.Core.Features.Shared.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.UserScore
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureUserScore<TRankingTableReader>(this IServiceCollection services)
            where TRankingTableReader : class, IRankingTableReader
        {
            // External dependency
            services.AddSingleton<IRankingTableReader, TRankingTableReader>();

            // CommandHandler

            // QueryHandler
            services.AddScoped<ISubscriber, UserScoreQueryHandler>();

            return services;
        }
    }
}
