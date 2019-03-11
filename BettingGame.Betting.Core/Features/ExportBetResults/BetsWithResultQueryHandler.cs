using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Features.ParticipantBet.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.ExportBetResults
{
    internal class BetsWithResultQueryHandler : ISubscriber
    {
        private readonly IBetReader _betReader;

        public BetsWithResultQueryHandler(IBetReader betReader)
        {
            _betReader = betReader;
        }

        [Subscribe]
        public async Task<IEnumerable<BetsWithResultQueryResult>> ExecuteAsync(BetsWithResultQuery query)
        {
            IEnumerable<Bet> result = await _betReader.GetBetsWithActualResult();
            return result.Select(bet => new BetsWithResultQueryResult { Id = bet.Id, Score = bet.GetScoreForBet(), UserId = bet.UserId });
        }
    }
}
