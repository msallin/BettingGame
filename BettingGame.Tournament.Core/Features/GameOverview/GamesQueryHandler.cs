using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.GameOverview.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.GameOverview
{
    internal class GamesQueryHandler : ISubscriber
    {
        private readonly IGameReader _reader;

        public GamesQueryHandler(IGameReader reader)
        {
            _reader = reader;
        }

        [Subscribe]
        public Task<IEnumerable<Game>> ExecuteAsync(GamesQuery query)
        {
            IQueryable<Game> queryable = _reader.QueryableGames();
            if (query.Type.HasValue)
            {
                queryable = queryable.Where(g => g.Type == query.Type);
            }

            if (query.StartDate.HasValue)
            {
                DateTime start = query.StartDate.Value.Date;
                DateTime end = start.AddDays(1).AddSeconds(-1);

                queryable = queryable.Where(g => g.StartDate >= start && g.StartDate < end);
            }

            if (!string.IsNullOrWhiteSpace(query.Group))
            {
                string group = query.Group.ToUpperInvariant();
                queryable = queryable.Where(g => g.Group == group);
            }


            // The sorting has to be in memory. It does not work when applied to the queryable.
            IEnumerable<Game> result = queryable.AsEnumerable().OrderBy(g => g.StartDate).ToList();
            var a = Task.FromResult(result);
            return a;
        }
    }
}
