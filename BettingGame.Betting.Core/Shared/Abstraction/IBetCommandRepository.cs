using System;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;

namespace BettingGame.Betting.Core.Shared.Abstraction
{
    public interface IBetCommandRepository : ICommandRepository<Bet>
    {
        Task<Bet> GetByGameIdAndUserIdAsync(Guid gameId, Guid userId);

        Task SetActualResultForGame(Guid gameId, Result result);
    }
}
