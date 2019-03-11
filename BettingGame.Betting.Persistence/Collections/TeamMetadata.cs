using MongoDbGenericRepository.Models;

namespace BettingGame.Betting.Persistence.Collections
{
    public class TeamMetadata : Core.Domain.TeamMetadata, IDocument
    {
        public int Version { get; set; }
    }
}
