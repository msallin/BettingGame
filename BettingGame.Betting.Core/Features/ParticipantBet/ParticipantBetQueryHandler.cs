using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Features.ParticipantBet.Abstraction;
using BettingGame.Framework.Extensions;
using BettingGame.Framework.Security;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.ParticipantBet
{
    internal class ParticipantBetQueryHandler : ISubscriber
    {
        private readonly IBetReader _betReader;

        private readonly IPrincipalProvider _principalProvider;

        public ParticipantBetQueryHandler(IBetReader betReader, IPrincipalProvider principalProvider)
        {
            _betReader = betReader;
            _principalProvider = principalProvider;
        }

        [Subscribe]
        public async Task<IEnumerable<ParticipantBetQueryResult>> ExecuteAsync(ParticipantBetQuery query)
        {
            Guid userId = _principalProvider.Get().GetUserId();
            if (query.GameId.HasValue)
            {
                var result = new[] { await _betReader.GetByUserIdAndGameId(query.GameId.Value, userId) };
                return ToQueryResult(result);
            }

            return ToQueryResult(await _betReader.GetByUserId(userId));
        }

        private static IEnumerable<ParticipantBetQueryResult> ToQueryResult(IEnumerable<Bet> bet)
        {
            return bet.Where(b => b != null).Select(b => new ParticipantBetQueryResult
            {
                ActualResult = b.ActualResult,
                BetResult = b.BetResult,
                Score = b.GetScoreForBet(),
                GameId = b.GameId
            });
        }
    }
}
