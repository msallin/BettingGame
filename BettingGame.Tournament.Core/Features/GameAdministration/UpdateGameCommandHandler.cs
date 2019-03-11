using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.GameAdministration.Abstraction;

using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    internal class UpdateGameCommandHandler : ISubscriber
    {
        private readonly IEventPublisher _eventPublisher;

        private readonly IGameCommandRepository _gameCommandRepository;

        public UpdateGameCommandHandler(IGameCommandRepository gameCommandRepository, IEventPublisher eventPublisher)
        {
            _gameCommandRepository = gameCommandRepository;
            _eventPublisher = eventPublisher;
        }

        [Subscribe]
        public async Task ExecuteAsync(UpdateGameCommand command)
        {
            // See also CreateGameCommandHandler.
            // This code is duplicated. Once you have to change it, consider to refactor and put onto domain.
            if (command.Type == GameType.Group)
            {
                if (!command.TeamA.HasValue)
                {
                    throw new ValidationException("TeamA must not be null for a group game.");
                }

                if (!command.TeamB.HasValue)
                {
                    throw new ValidationException("TeamB must not be null for a group game.");
                }
            }

            Game updatedGame = await _gameCommandRepository.UpdateAsync(command.Id, game =>
            {
                game.StartDate = command.StartDate ?? game.StartDate;
                game.TeamA = command.TeamA ?? game.TeamA;
                game.TeamB = command.TeamB ?? game.TeamB;
                game.Group = command.Group ?? game.Group;
                game.Type = command.Type;
            });

            await _eventPublisher.PublishAsync(new GameChangedEvent { Game = updatedGame });
        }
    }
}
