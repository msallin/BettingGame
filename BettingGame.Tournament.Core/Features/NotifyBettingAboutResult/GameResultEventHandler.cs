using System.Threading.Tasks;

using BettingGame.Framework.Abstraction.Clients.Betting;
using BettingGame.Tournament.Core.Domain;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.NotifyBettingAboutResult
{
    public class GameResultEventHandler : ISubscriber
    {
        private readonly IBettingClient _bettingClient;

        public GameResultEventHandler(IBettingClient bettingClient)
        {
            _bettingClient = bettingClient;
        }

        [Subscribe]
        public Task ExecuteAsync(GameResultEvent @event)
        {
            return _bettingClient.ApiResultPostAsync(new SetActualResultCommand { GameId = @event.GameId, ScoreTeamA = @event.ScoreTeamA, ScoreTeamB = @event.ScoreTeamB });
        }
    }
}
