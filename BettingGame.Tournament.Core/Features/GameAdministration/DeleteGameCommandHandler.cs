using System.Threading.Tasks;

using BettingGame.Tournament.Core.Features.GameAdministration.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    internal class DeleteGameCommandHandler : ISubscriber
    {
        private readonly IGameCommandRepository _gameCommandRepository;

        public DeleteGameCommandHandler(IGameCommandRepository gameCommandRepository)
        {
            _gameCommandRepository = gameCommandRepository;
        }

        [Subscribe]
        public Task ExecuteAsync(DeleteGameCommand command)
        {
            return _gameCommandRepository.DeleteAsync(command.Id);
        }
    }
}
