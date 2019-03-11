using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Features.GameHandling.Abstraction;
using BettingGame.Betting.Persistence.Collections;
using BettingGame.Framework.MongoDb;

using MongoDB.Driver;

namespace BettingGame.Betting.Persistence.Write
{
    public class GameMetadataCommandRepository : Repository<GameMetadata, Core.Domain.GameMetadata>, IGameMetadataCommandRepository
    {
        public GameMetadataCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }

        public async Task<Core.Domain.GameMetadata> UpsertAsync(Guid id, Action<Core.Domain.GameMetadata> setValues)
        {
            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<GameMetadata> filter = new ExpressionFilterDefinition<GameMetadata>(collection => collection.Id == id);
            GameMetadata documentToUpdate = await dbContext.GetCollection<GameMetadata>().Find(filter).SingleOrDefaultAsync();
            if (documentToUpdate != null)
            {
                setValues(documentToUpdate);
                await BeforeUpdate(documentToUpdate, dbContext);
                await dbContext.GetCollection<GameMetadata>().ReplaceOneAsync(filter, documentToUpdate);
            }

            if (documentToUpdate == null)
            {
                documentToUpdate = new GameMetadata();
                setValues(documentToUpdate);
                await BeforeInsert(documentToUpdate, dbContext);
                await dbContext.GetCollection<GameMetadata>().InsertOneAsync(documentToUpdate);
            }

            return documentToUpdate;
        }

        public async Task<IEnumerable<Core.Domain.GameMetadata>> GetByDateAsync(DateTimeOffset date)
        {
            DateTimeOffset min = date;
            DateTimeOffset max = date.AddDays(1);

            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<GameMetadata> filter = new ExpressionFilterDefinition<GameMetadata>(collection => collection.StartDate > min && collection.StartDate < max);
            return await dbContext.GetCollection<GameMetadata>().Find(filter).ToListAsync();
        }
    }
}
