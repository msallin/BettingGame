using System;
using System.Collections.Generic;

namespace BettingGame.Ranking.Core.Domain
{
    public class RankingSnapshot
    {
        public Guid Id { get; set; }

        public IEnumerable<RankingEntry> RankingEntries { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
