using MongoDbGenericRepository.Models;

namespace BettingGame.Betting.Persistence.Collections
{
    public class GameMetadata : Core.Domain.GameMetadata, IDocument
    {
        public int Version { get; set; }
    }
}
