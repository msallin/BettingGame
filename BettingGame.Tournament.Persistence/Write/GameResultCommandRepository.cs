using System;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.ResultAdministration.Abstraction;

namespace BettingGame.Tournament.Persistence.Write
{
    public class GameResultCommandRepository : Repository<Collections.Game, Core.Domain.Game>, IGameResultCommandRepository
    {
        public GameResultCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }

        public Task UpsertAsync(Guid gameId, Result result)
        {
            return UpdateAsync(gameId, game => game.Result = result);
        }
    }
}
