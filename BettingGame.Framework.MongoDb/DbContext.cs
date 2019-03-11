using Microsoft.Extensions.Options;

using MongoDbGenericRepository;

namespace BettingGame.Framework.MongoDb
{
    public class DbContext : MongoDbContext
    {
        public DbContext(IOptions<MongoDbOptions> mongoDbOptions)
            : base(mongoDbOptions.Value.ConnectionString, mongoDbOptions.Value.Database)
        { }
    }
}
