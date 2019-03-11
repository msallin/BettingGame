using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.ResultAdministration.Abstraction;

using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.ResultAdministration
{
    internal class SetGameResultCommandHandler : ISubscriber
    {
        private readonly IEventPublisher _eventPublisher;

        private readonly IGameResultCommandRepository _gameResultCommandRepository;

        public SetGameResultCommandHandler(IGameResultCommandRepository gameResultCommandRepository, IEventPublisher eventPublisher)
        {
            _gameResultCommandRepository = gameResultCommandRepository;
            _eventPublisher = eventPublisher;
        }

        [Subscribe]
        public async Task ExecuteAsync(SetGameResultCommand command)
        {
            await _gameResultCommandRepository.UpsertAsync(command.GameId, new Result { ScoreTeamA = command.ScoreTeamA, ScoreTeamB = command.ScoreTeamB });
            await _eventPublisher.PublishAsync(new GameResultEvent { GameId = command.GameId, ScoreTeamA = command.ScoreTeamA, ScoreTeamB = command.ScoreTeamB });
        }
    }
}
