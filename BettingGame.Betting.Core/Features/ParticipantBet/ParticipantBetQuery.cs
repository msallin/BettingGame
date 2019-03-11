using System;
using System.Collections.Generic;

using Silverback.Messaging.Messages;

namespace BettingGame.Betting.Core.Features.ParticipantBet
{
    public class ParticipantBetQuery : IQuery<IEnumerable<ParticipantBetQueryResult>>
    {
        public Guid? GameId { get; set; }
    }
}
