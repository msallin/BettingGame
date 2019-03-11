using System.Linq;

using BettingGame.Tournament.Core.Domain;

namespace BettingGame.Tournament.Core.Features.GameOverview.Abstraction
{
    public interface IGameReader
    {
        IQueryable<Game> QueryableGames();
    }
}
