using MongoDbGenericRepository.Models;

namespace BettingGame.Betting.Persistence.Collections
{
    public class Bet : Core.Domain.Bet, IDocument
    {
        public int Version { get; set; }
    }
}
