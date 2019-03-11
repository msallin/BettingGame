using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.Tournament.Core.Features.GameAdministration.Abstraction;
using BettingGame.Tournament.Persistence.Collections;

using MongoDB.Driver;

namespace BettingGame.Tournament.Persistence.Write
{
    public class GameCommandRepository : Repository<Game, Core.Domain.Game>, IGameCommandRepository
    {
        public GameCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }

        protected override async Task BeforeInsert(Game record, DbContext context)
        {
            await base.BeforeInsert(record, context);

            await ValidateTeamReferences(record, context);
        }

        protected override async Task BeforeUpdate(Game record, DbContext context)
        {
            await base.BeforeUpdate(record, context);

            await ValidateTeamReferences(record, context);
        }

        private static async Task ValidateTeamReferences(Game record, DbContext context)
        {
            // Of course this is a race condition but teams are not deleted often, so its okay.
            if (record.TeamA.HasValue)
            {
                long countTeamA = await context.GetCollection<Team>().Find(new ExpressionFilterDefinition<Team>(f => f.Id == record.TeamA)).CountDocumentsAsync();
                if (countTeamA < 1)
                {
                    throw new ValidationException("TeamA does not exist.");
                }
            }

            if (record.TeamB.HasValue)
            {
                long countTeamB = await context.GetCollection<Team>().Find(new ExpressionFilterDefinition<Team>(f => f.Id == record.TeamB)).CountDocumentsAsync();
                if (countTeamB < 1)
                {
                    throw new ValidationException("TeamB does not exist.");
                }
            }
        }
    }
}
