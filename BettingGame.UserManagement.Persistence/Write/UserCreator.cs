using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BettingGame.Framework;
using BettingGame.Framework.MongoDb;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Registration.Abstraction;

using MongoDB.Driver;

namespace BettingGame.UserManagement.Persistence.Write
{
    public class UserCreator : IUserCreator
    {
        private readonly DbContextFactory _dbContextFactory;

        public UserCreator(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public User Create()
        {
            return new Collections.User();
        }

        public async Task SaveAsync(User user)
        {
            try
            {
                await _dbContextFactory.Create().GetCollection<Collections.User>().InsertOneAsync((Collections.User)user);
            }
            catch (MongoWriteException exception)
            {
                if (exception.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    throw new DuplicatedEmailException();
                }

                throw;
            }
        }
    }
}
