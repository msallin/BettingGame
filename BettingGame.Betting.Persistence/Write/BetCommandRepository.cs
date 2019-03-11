using System;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Shared.Abstraction;
using BettingGame.Framework.MongoDb;

using MongoDB.Driver;

namespace BettingGame.Betting.Persistence.Write
{
    public class BetCommandRepository : Repository<Collections.Bet, Core.Domain.Bet>, IBetCommandRepository
    {
        public BetCommandRepository(DbContextFactory dbContextFactory)
            : base(dbContextFactory)
        { }

        public async Task<Core.Domain.Bet> GetByGameIdAndUserIdAsync(Guid gameId, Guid userId)
        {
            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<Collections.Bet> filter = new ExpressionFilterDefinition<Collections.Bet>(b => b.GameId == gameId && b.UserId == userId);
            Collections.Bet bet = await dbContext.GetCollection<Collections.Bet>().Find(filter).SingleOrDefaultAsync();
            return bet;
        }

        public Task SetActualResultForGame(Guid gameId, Result result)
        {
            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<Collections.Bet> filter = Builders<Collections.Bet>.Filter.Eq(s => s.GameId, gameId);
            UpdateDefinition<Collections.Bet> update = Builders<Collections.Bet>.Update.Set(s => s.ActualResult, result);
            return dbContext.GetCollection<Collections.Bet>().UpdateManyAsync(filter, update);
        }
    }
}
