using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Domain
{
    public class GameChangedEvent : IEvent
    {
        public Game Game { get; set; }
    }
}
