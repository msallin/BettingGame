using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.TeamOverview.Abstraction;

using MongoDB.Driver;

namespace BettingGame.Tournament.Persistence.Read
{
    public class TeamReader : ITeamReader
    {
        private readonly DbContextFactory _dbContextFactory;

        public TeamReader(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Team>> AllAsync()
        {
            IEnumerable<Collections.Team> result = await _dbContextFactory.Create().GetCollection<Collections.Team>().Find(FilterDefinition<Collections.Team>.Empty).ToListAsync();
            return result;
        }
    }
}
