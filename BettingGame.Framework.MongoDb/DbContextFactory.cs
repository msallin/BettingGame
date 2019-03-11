using Microsoft.Extensions.Options;

namespace BettingGame.Framework.MongoDb
{
    public class DbContextFactory
    {
        private readonly IOptions<MongoDbOptions> _options;

        public DbContextFactory(IOptions<MongoDbOptions> options)
        {
            _options = options;
        }

        public DbContext Create()
        {
            return new DbContext(_options);
        }
    }
}
