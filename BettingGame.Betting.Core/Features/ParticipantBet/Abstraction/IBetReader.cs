using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;

namespace BettingGame.Betting.Core.Features.ParticipantBet.Abstraction
{
    public interface IBetReader
    {
        Task<IEnumerable<Bet>> GetBetsWithActualResult();

        Task<IEnumerable<Bet>> GetByUserId(Guid userId);

        Task<Bet> GetByUserIdAndGameId(Guid gameId, Guid userId);
    }
}
