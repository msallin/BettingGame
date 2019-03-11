using System.Threading.Tasks;

using BettingGame.Betting.Core.Features.TeamHandling.Abstraction;
using BettingGame.DomainEvents;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.TeamHandling
{
    internal class TeamChangedEventHandler : ISubscriber
    {
        private readonly ITeamMetadataCommandRepository _repository;

        public TeamChangedEventHandler(ITeamMetadataCommandRepository repository)
        {
            _repository = repository;
        }

        [Subscribe]
        public async Task ExecuteAsync(TeamChangedEvent command)
        {
            await _repository.UpsertAsync(command.Id, metadata =>
            {
                metadata.Id = command.Id;
                metadata.FifaCode = command.FifaCode;
            });
        }
    }
}
