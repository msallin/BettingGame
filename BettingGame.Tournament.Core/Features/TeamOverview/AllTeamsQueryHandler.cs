using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.TeamOverview.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.TeamOverview
{
    internal class AllTeamsQueryHandler : ISubscriber
    {
        private readonly ITeamReader _reader;

        public AllTeamsQueryHandler(ITeamReader reader)
        {
            _reader = reader;
        }

        [Subscribe]
        public Task<IEnumerable<Team>> ExecuteAsync(AllTeamsQuery query)
        {
            return _reader.AllAsync();
        }
    }
}
