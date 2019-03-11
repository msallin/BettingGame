using MongoDbGenericRepository.Models;

namespace BettingGame.Tournament.Persistence.Collections
{
    public class Game : Core.Domain.Game, IDocument
    {
        public int Version { get; set; }
    }
}
