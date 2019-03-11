using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Framework;
using BettingGame.Framework.MongoDb;
using BettingGame.UserManagement.Persistence.Collections;

using MongoDB.Bson;
using MongoDB.Driver;

namespace BettingGame.UserManagement.Persistence
{
    public class MongoDbStartup : IStartupTask
    {
        private readonly DbContextFactory _dbContextFactory;

        public MongoDbStartup(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task Run()
        {
            DbContext dbContext = _dbContextFactory.Create();

            await EnsureEmailUniqueIndex(dbContext);
        }

        private static async Task EnsureEmailUniqueIndex(DbContext dbContext)
        {
            var emailIndexExists = false;
            IAsyncCursor<BsonDocument> indexes = dbContext.GetCollection<User>().Indexes.List();
            List<BsonDocument> list = await indexes.ToListAsync();
            foreach (BsonDocument bsonDocument in list)
            {
                if (bsonDocument.Elements.Any(e => e.Name == "key" && e.Value.AsBsonDocument.Names.FirstOrDefault() == "Email"))
                {
                    emailIndexExists = true;
                }
            }

            if (!emailIndexExists)
            {
                await dbContext.GetCollection<User>().Indexes.CreateOneAsync(new CreateIndexModel<User>("{ \"Email\": 1 }", new CreateIndexOptions { Unique = true }));
            }
        }
    }
}
