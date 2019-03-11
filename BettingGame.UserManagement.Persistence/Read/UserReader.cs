using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework.MongoDb;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using MongoDB.Driver;

namespace BettingGame.UserManagement.Persistence.Read
{
    public class UserReader : IUserReader
    {
        private readonly DbContextFactory _dbContextFactory;

        public UserReader(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<User>> AllAsync()
        {
            DbContext dbContext = _dbContextFactory.Create();
            IEnumerable<Collections.User> users = await dbContext.GetCollection<Collections.User>().Find(FilterDefinition<Collections.User>.Empty).ToListAsync();
            return users;
        }

        public async Task<User> ByEmail(string email)
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.User> filter = new ExpressionFilterDefinition<Collections.User>(u => u.Email == email);
            Collections.User user = await dbContext.GetCollection<Collections.User>().Find(filter).SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> ByIdAsync(Guid id)
        {
            DbContext dbContext = _dbContextFactory.Create();
            FilterDefinition<Collections.User> filter = new ExpressionFilterDefinition<Collections.User>(u => u.Id == id);
            Collections.User user = await dbContext.GetCollection<Collections.User>().Find(filter).SingleOrDefaultAsync();
            return user;
        }
    }
}
