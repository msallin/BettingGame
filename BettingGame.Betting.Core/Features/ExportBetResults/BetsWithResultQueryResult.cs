using System;

namespace BettingGame.Betting.Core.Features.ExportBetResults
{
    public class BetsWithResultQueryResult
    {
        public Guid Id { get; set; }

        public int Score { get; set; }

        public Guid UserId { get; set; }
    }
}
