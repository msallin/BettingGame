using System;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.UserProfile.Abstraction;

using MongoDB.Driver;

namespace BettingGame.UserManagement.Persistence.Write
{
    public class UserUpdater : IUserUpdater
    {
        private readonly DbContextFactory _dbContextFactory;

        public UserUpdater(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<User> GetAsync(Guid id)
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.User> filter = new ExpressionFilterDefinition<Collections.User>(u => u.Id == id);
            Collections.User user = await dbContext.GetCollection<Collections.User>().Find(filter).SingleOrDefaultAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.User> filter = new ExpressionFilterDefinition<Collections.User>(u => u.Id == user.Id);
            await dbContext.GetCollection<Collections.User>().ReplaceOneAsync(filter, (Collections.User)user);
        }
    }
}
