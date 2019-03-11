using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Ranking.Core.Domain;

namespace BettingGame.Ranking.Core.Features.Shared.Abstraction
{
    public interface IRankingTableReader
    {
        Task<IEnumerable<RankingEntry>> GetCurrentRankingAsync();
    }
}
