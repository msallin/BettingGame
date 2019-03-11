using MongoDbGenericRepository.Models;

namespace BettingGame.Ranking.Persistence.Collections
{
    public class RankingSnapshot : Core.Domain.RankingSnapshot, IDocument
    {
        public int Version { get; set; }
    }
}
