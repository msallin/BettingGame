using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Features.Shared.Abstraction;

using Microsoft.Extensions.Caching.Memory;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.RankingTable
{
    internal class RankingTableQueryHandler : ISubscriber
    {
        private readonly IMemoryCache _cache;

        private readonly IRankingTableReader _reader;

        public RankingTableQueryHandler(IRankingTableReader reader, IMemoryCache cache)
        {
            _reader = reader;
            _cache = cache;
        }

        private static TimeSpan CacheTime => TimeSpan.FromSeconds(60);

        [Subscribe]
        public async Task<RankingTableQueryResult> ExecuteAsync(RankingTableQuery query)
        {
            string cacheKey = typeof(RankingTableQuery).FullName;

            if (!_cache.TryGetValue(cacheKey, out RankingTableQueryResult result))
            {
                IEnumerable<RankingEntry> currentRanking = await _reader.GetCurrentRankingAsync();
                result = new RankingTableQueryResult(currentRanking);

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(CacheTime);
                _cache.Set(cacheKey, result, cacheEntryOptions);
            }

            return result;
        }
    }
}
