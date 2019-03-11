using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BettingGame.Framework.Abstraction.Clients.UserManagement;
using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Features.RefreshRanking.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.RefreshRanking
{
    internal class NewRankingSnapshotEventHandler : ISubscriber
    {
        private readonly IRankingSnapshotCommandRepository _repository;

        private readonly IUserManagementClient _userManagementClient;

        public NewRankingSnapshotEventHandler(IUserManagementClient userManagementClient, IRankingSnapshotCommandRepository repository)
        {
            _userManagementClient = userManagementClient;
            _repository = repository;
        }

        [Subscribe]
        public async Task ExecuteAsync(NewRankingSnapshotEvent @event)
        {
            RankingSnapshot rankingSnapshot = await _repository.GetAsync(@event.Id);
            List<RankingEntry> ordered = rankingSnapshot.RankingEntries.OrderByDescending(entry => entry.Score).ToList();

            for (var i = 0; i < ordered.Count; i++)
            {
                RankingEntry entry = ordered[i];

                // This could be batched if we assume that every user is in the ranking.
                IEnumerable<Profile> profiles = await _userManagementClient.ApiUserGetAsync(entry.UserId);
                Profile profile = profiles.Single();

                entry.Rank = i + 1;
                entry.UserFirstName = profile.FirstName;
                entry.UserLastName = profile.LastName;
                entry.UserNickname = profile.Nickname;
                entry.UserGravatarHash = GravatarHashCreator.HashEmailForGravatar(profile.Email);
            }

            await _repository.UpdateAsync(@event.Id, snapshot => snapshot.RankingEntries = rankingSnapshot.RankingEntries);
        }
    }
}
