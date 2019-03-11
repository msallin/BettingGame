using System.Collections.Generic;

using BettingGame.Tournament.Core.Domain;

using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Features.TeamOverview
{
    public class AllTeamsQuery : IQuery<IEnumerable<Team>>
    { }
}
