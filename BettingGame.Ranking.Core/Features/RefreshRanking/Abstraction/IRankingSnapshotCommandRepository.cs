using System.Threading.Tasks;

using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Shared.Abstraction;

namespace BettingGame.Ranking.Core.Features.RefreshRanking.Abstraction
{
    public interface IRankingSnapshotCommandRepository : ICommandRepository<RankingSnapshot>
    {
        Task<RankingSnapshot> GetNewestAsync();
    }
}
