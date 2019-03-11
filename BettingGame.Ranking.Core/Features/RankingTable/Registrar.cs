using BettingGame.Ranking.Core.Features.Shared.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.RankingTable
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureRankingTable<TRankingTableReader>(this IServiceCollection services)
            where TRankingTableReader : class, IRankingTableReader
        {
            // External dependency
            services.AddSingleton<IRankingTableReader, TRankingTableReader>();

            // CommandHandler

            // QueryHandler
            services.AddScoped<ISubscriber, RankingTableQueryHandler>();

            return services;
        }
    }
}
