using MongoDbGenericRepository.Models;

namespace BettingGame.Tournament.Persistence.Collections
{
    public class Team : Core.Domain.Team, IDocument
    {
        public int Version { get; set; }
    }
}
