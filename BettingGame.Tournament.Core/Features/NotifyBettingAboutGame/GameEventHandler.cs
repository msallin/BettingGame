using System.Threading.Tasks;

using BettingGame.Framework.Abstraction.Clients.Betting;
using BettingGame.Tournament.Core.Domain;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.NotifyBettingAboutGame
{
    public class GameChangedEventHandler : ISubscriber
    {
        private readonly IBettingClient _bettingClient;

        public GameChangedEventHandler(IBettingClient bettingClient)
        {
            _bettingClient = bettingClient;
        }

        [Subscribe]
        public Task ExecuteAsync(GameChangedEvent @event)
        {
            return _bettingClient.ApiGameMetadataPostAsync(new SaveGameMetadata { Id = @event.Game.Id, StartDate = @event.Game.StartDate, TeamA = @event.Game.TeamA, TeamB = @event.Game.TeamB });
        }
    }
}
