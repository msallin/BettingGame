using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Features.Shared.Abstraction;

using Microsoft.Extensions.Caching.Memory;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.UserScore
{
    internal class UserScoreQueryHandler : ISubscriber
    {
        private readonly IMemoryCache _cache;

        private readonly IRankingTableReader _rankingTableReader;

        public UserScoreQueryHandler(IRankingTableReader rankingTableReader, IMemoryCache cache)
        {
            _rankingTableReader = rankingTableReader;
            _cache = cache;
        }

        private static TimeSpan CacheTime => TimeSpan.FromSeconds(60);

        [Subscribe]
        public async Task<RankingEntry> ExecuteAsync(UserScoreQuery query)
        {
            string cacheKey = typeof(UserScoreQuery).FullName + query.UserId;

            if (!_cache.TryGetValue(cacheKey, out RankingEntry rankingEntry))
            {
                IEnumerable<RankingEntry> result = await _rankingTableReader.GetCurrentRankingAsync();
                rankingEntry = result.FirstOrDefault(r => r.UserId == query.UserId);

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(CacheTime);
                _cache.Set(cacheKey, rankingEntry, cacheEntryOptions);
            }

            return rankingEntry;
        }
    }
}
