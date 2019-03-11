using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;

namespace BettingGame.Tournament.Core.Features.TeamOverview.Abstraction
{
    public interface ITeamReader
    {
        Task<IEnumerable<Team>> AllAsync();
    }
}
