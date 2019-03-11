using MongoDbGenericRepository.Models;

namespace BettingGame.UserManagement.Persistence.Collections
{
    internal class User : Core.Domain.User, IDocument
    {
        public int Version { get; set; }
    }
}
