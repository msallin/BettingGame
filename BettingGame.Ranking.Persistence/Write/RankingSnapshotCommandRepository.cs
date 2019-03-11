using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.Ranking.Core.Features.RefreshRanking.Abstraction;
using BettingGame.Ranking.Persistence.Collections;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BettingGame.Ranking.Persistence.Write
{
    public class RankingSnapshotCommandRepository : Repository<RankingSnapshot, Core.Domain.RankingSnapshot>, IRankingSnapshotCommandRepository
    {
        public RankingSnapshotCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }

        public async Task<Core.Domain.RankingSnapshot> GetNewestAsync()
        {
            return await DbContextFactory.Create().GetCollection<RankingSnapshot>()
                .AsQueryable()
                .OrderByDescending(r => r.Timestamp)
                .FirstOrDefaultAsync();
        }
    }
}
