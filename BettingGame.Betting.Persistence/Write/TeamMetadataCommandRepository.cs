using System;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Features.TeamHandling.Abstraction;
using BettingGame.Framework.MongoDb;

using MongoDB.Driver;

namespace BettingGame.Betting.Persistence.Write
{
    public class TeamMetadataCommandRepository : Repository<Collections.TeamMetadata, TeamMetadata>, ITeamMetadataCommandRepository
    {
        public TeamMetadataCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }

        public async Task<TeamMetadata> UpsertAsync(Guid id, Action<TeamMetadata> setValues)
        {
            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<Collections.TeamMetadata> filter = new ExpressionFilterDefinition<Collections.TeamMetadata>(collection => collection.Id == id);
            Collections.TeamMetadata documentToUpdate = await dbContext.GetCollection<Collections.TeamMetadata>().Find(filter).SingleOrDefaultAsync();
            if (documentToUpdate != null)
            {
                setValues(documentToUpdate);
                await BeforeUpdate(documentToUpdate, dbContext);
                await dbContext.GetCollection<Collections.TeamMetadata>().ReplaceOneAsync(filter, documentToUpdate);
            }

            if (documentToUpdate == null)
            {
                documentToUpdate = new Collections.TeamMetadata();
                setValues(documentToUpdate);
                await BeforeInsert(documentToUpdate, dbContext);
                await dbContext.GetCollection<Collections.TeamMetadata>().InsertOneAsync(documentToUpdate);
            }

            return documentToUpdate;
        }
    }
}
