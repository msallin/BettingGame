using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Framework.Abstraction.Clients.Betting;
using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Features.RefreshRanking.Abstraction;

using Microsoft.Extensions.Logging;

using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.RefreshRanking
{
    internal class RefreshRankingCommandHandler : ISubscriber
    {
        private readonly IBettingClient _bettingClient;

        private readonly IEventPublisher _eventPublisher;

        private readonly ILogger _logger;

        private readonly IRankingSnapshotCommandRepository _repository;

        public RefreshRankingCommandHandler(IBettingClient bettingClient, IRankingSnapshotCommandRepository repository, ILogger<RefreshRankingCommand> logger, IEventPublisher eventPublisher)
        {
            _bettingClient = bettingClient;
            _repository = repository;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        [Subscribe]
        public async Task ExecuteAsync(RefreshRankingCommand command)
        {
            try
            {
                // This is maybe a little bit to brute force...!
                // If performance problems are faced, consider to only get the delta.
                IEnumerable<BetsWithResultQueryResult> result = await _bettingClient.ApiBetScoreGetAsync();
                IEnumerable<RankingEntry> ranking = Calculate(result);
                Guid? id = await Persist(ranking);
                if (id.HasValue)
                {
                    await _eventPublisher.PublishAsync(new NewRankingSnapshotEvent { Id = id.Value });
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to refresh ranking.");
            }
        }

        private static IEnumerable<RankingEntry> Calculate(IEnumerable<BetsWithResultQueryResult> result)
        {
            IEnumerable<RankingEntry> entries = result.GroupBy(r => r.UserId.Value).Select(r => new RankingEntry
            {
                UserId = r.Key,
                Score = r.Sum(g => g.Score.Value)
            });

            return entries;
        }

        private async Task<Guid?> Persist(IEnumerable<RankingEntry> result)
        {
            RankingSnapshot newest = await _repository.GetNewestAsync();

            if (!result.Any())
            {
                return null;
            }

            if (newest != null && newest.RankingEntries.OrderByDescending(r => r.Score).SequenceEqual(result.OrderByDescending(r => r.Score), new RankingEntryComparer()))
            {
                return null;
            }

            RankingSnapshot snapshot = await _repository.AddAsync(rankingSnapshot =>
            {
                rankingSnapshot.RankingEntries = result;
                rankingSnapshot.Timestamp = DateTimeOffset.UtcNow;
            });

            return snapshot.Id;
        }

        private class RankingEntryComparer : IEqualityComparer<RankingEntry>
        {
            public bool Equals(RankingEntry x, RankingEntry y)
            {
                if (x == null)
                {
                    return false;
                }

                if (y == null)
                {
                    return false;
                }

                return x.Score == y.Score && x.UserId == y.UserId;
            }

            public int GetHashCode(RankingEntry obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
