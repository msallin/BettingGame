using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.Shared.Abstraction;

namespace BettingGame.Tournament.Core.Features.TeamAdministration.Abstraction
{
    public interface ITeamCommandRepository : ICommandRepository<Team>
    { }
}
