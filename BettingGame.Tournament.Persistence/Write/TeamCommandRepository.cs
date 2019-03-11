using BettingGame.Framework.MongoDb;
using BettingGame.Tournament.Core.Features.TeamAdministration.Abstraction;
using BettingGame.Tournament.Persistence.Collections;

namespace BettingGame.Tournament.Persistence.Write
{
    public class TeamCommandRepository : Repository<Team, Core.Domain.Team>, ITeamCommandRepository
    {
        public TeamCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }
    }
}
