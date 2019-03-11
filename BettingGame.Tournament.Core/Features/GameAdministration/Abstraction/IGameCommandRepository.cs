using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.Shared.Abstraction;

namespace BettingGame.Tournament.Core.Features.GameAdministration.Abstraction
{
    public interface IGameCommandRepository : ICommandRepository<Game>
    { }
}
