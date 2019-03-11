using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Features.Shared.Abstraction;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BettingGame.Ranking.Persistence.Read
{
    public class RankingTableReader : IRankingTableReader
    {
        private readonly DbContextFactory _dbContextFactory;

        public RankingTableReader(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<RankingEntry>> GetCurrentRankingAsync()
        {
            var result = await _dbContextFactory.Create()
                .GetCollection<Collections.RankingSnapshot>()
                .AsQueryable()
                .OrderByDescending(r => r.Timestamp)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return Enumerable.Empty<RankingEntry>();
            }

            return result.RankingEntries;
        }
    }
}
