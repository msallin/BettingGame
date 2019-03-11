using System.Threading.Tasks;

using BettingGame.Betting.Core.Features.GameHandling.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.GameHandling
{
    internal class SaveGameMetadataCommandHandler : ISubscriber
    {
        private readonly IGameMetadataCommandRepository _repository;

        public SaveGameMetadataCommandHandler(IGameMetadataCommandRepository repository)
        {
            _repository = repository;
        }

        [Subscribe]
        public async Task ExecuteAsync(SaveGameMetadata command)
        {
            await _repository.UpsertAsync(command.Id, metadata =>
            {
                metadata.Id = command.Id;
                metadata.TeamB = command.TeamB;
                metadata.TeamA = command.TeamA;
                metadata.StartDate = command.StartDate;
            });
        }
    }
}
