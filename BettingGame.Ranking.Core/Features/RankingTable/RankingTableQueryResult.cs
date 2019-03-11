using System;
using System.Collections.Generic;
using System.Linq;

using BettingGame.Ranking.Core.Domain;

namespace BettingGame.Ranking.Core.Features.RankingTable
{
    public class RankingTableQueryResult
    {
        public RankingTableQueryResult(IEnumerable<RankingEntry> rankingEntries)
        {
            Entries = rankingEntries.OrderBy(r => r.Rank).Select(e => new Entry
            {
                Rank = e.Rank,
                Score = e.Score,
                UserId = e.UserId,
                NickName = e.UserNickname,
                GravatarHash = e.UserGravatarHash
            });
        }

        public IEnumerable<Entry> Entries { get; set; }

        public class Entry
        {
            public string NickName { get; set; }

            public int Rank { get; set; }

            public int Score { get; set; }

            public string GravatarHash { get; set; }

            public Guid UserId { get; set; }
        }
    }
}
