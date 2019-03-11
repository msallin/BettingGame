using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.GameOverview.Abstraction;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BettingGame.Tournament.Persistence.Read
{
    public class GameReader : IGameReader
    {
        private readonly DbContextFactory _dbContextFactory;

        public GameReader(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IQueryable<Game> QueryableGames()
        {
            IQueryable<Game> q = _dbContextFactory.Create().GetCollection<Collections.Game>().AsQueryable();
            return q;
        }

        public async Task<IEnumerable<Game>> ReadFromQueryable(IQueryable<Game> queryable)
        {
            return await ((IMongoQueryable<Game>)queryable).ToListAsync();
        }
    }
}
