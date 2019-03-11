using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.GameAdministration.Abstraction;

using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    internal class CreateGameCommandHandler : ISubscriber
    {
        private readonly IEventPublisher _eventPublisher;

        private readonly IGameCommandRepository _gameCommandRepository;

        public CreateGameCommandHandler(IGameCommandRepository gameCommandRepository, IEventPublisher eventPublisher)
        {
            _gameCommandRepository = gameCommandRepository;
            _eventPublisher = eventPublisher;
        }

        [Subscribe]
        public async Task ExecuteAsync(CreateGameCommand command)
        {
            Validate(command);

            Game createdGame = await _gameCommandRepository.AddAsync(game =>
            {
                game.StartDate = command.Start;
                game.TeamA = command.TeamA;
                game.TeamB = command.TeamB;
                game.Type = command.Type;
                game.Group = command.Group;
            });

            await _eventPublisher.PublishAsync(new GameChangedEvent { Game = createdGame });
        }

        private static void Validate(CreateGameCommand command)
        {
            // See also UpdateGameCommandHandler.
            // This code is duplicated. Once you have to change it, consider to refactor and put onto domain.
            if (command.Start.ToUniversalTime() < DateTimeOffset.UtcNow)
            {
                throw new ValidationException("The start of the game must not be in the past.");
            }

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
        }
    }
}
