using System;

using BettingGame.Betting.Core.Domain;

namespace BettingGame.Betting.Core.Features.ParticipantBet
{
    public class ParticipantBetQueryResult
    {
        public Result ActualResult { get; set; }

        public Result BetResult { get; set; }

        public Guid GameId { get; set; }

        public int Score { get; set; }
    }
}
