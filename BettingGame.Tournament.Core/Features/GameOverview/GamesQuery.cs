using System;
using System.Collections.Generic;

using BettingGame.Tournament.Core.Domain;

using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Features.GameOverview
{
    public class GamesQuery : IQuery<IEnumerable<Game>>
    {
        public GamesQuery(string group, GameType? type, DateTimeOffset? startDate)
        {
            Group = group;
            Type = type;
            StartDate = startDate;
        }

        public string Group { get; }

        public DateTimeOffset? StartDate { get; }

        public GameType? Type { get; }
    }
}
