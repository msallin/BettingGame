using System;

using Silverback.Messaging.Messages;

namespace BettingGame.Ranking.Core.Domain
{
    public class NewRankingSnapshotEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}
