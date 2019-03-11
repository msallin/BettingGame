using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Features.ParticipantBet.Abstraction;
using BettingGame.Framework.MongoDb;

using MongoDB.Driver;

namespace BettingGame.Betting.Persistence.Read
{
    public class BetReader : IBetReader
    {
        private readonly DbContextFactory _dbContextFactory;

        public BetReader(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Bet>> GetBetsWithActualResult()
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.Bet> filter = new ExpressionFilterDefinition<Collections.Bet>(b => b.ActualResult != null);
            return await dbContext.GetCollection<Collections.Bet>().Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Bet>> GetByUserId(Guid userId)
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.Bet> filter = new ExpressionFilterDefinition<Collections.Bet>(b => b.UserId == userId);
            return await dbContext.GetCollection<Collections.Bet>().Find(filter).ToListAsync();
        }

        public async Task<Bet> GetByUserIdAndGameId(Guid gameId, Guid userId)
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.Bet> filter = new ExpressionFilterDefinition<Collections.Bet>(b => b.GameId == gameId && b.UserId == userId);
            return await dbContext.GetCollection<Collections.Bet>().Find(filter).SingleOrDefaultAsync();
        }
    }
}
