using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Features.GameHandling.Abstraction;
using BettingGame.Betting.Core.Shared.Abstraction;
using BettingGame.Framework.Extensions;
using BettingGame.Framework.Security;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.ParticipantBet
{
    internal class ParticipantBetCommandHandler : ISubscriber
    {
        private readonly IGameMetadataCommandRepository _gameMetadataCommandRepository;

        private readonly IPrincipalProvider _principalProvider;

        private readonly IBetCommandRepository _repository;

        public ParticipantBetCommandHandler(IBetCommandRepository repository, IGameMetadataCommandRepository gameMetadataCommandRepository, IPrincipalProvider principalProvider)
        {
            _repository = repository;
            _gameMetadataCommandRepository = gameMetadataCommandRepository;
            _principalProvider = principalProvider;
        }

        [Subscribe]
        public Task ExecuteAsync(ParticipantBetCommand command)
        {
            Guid userId = _principalProvider.Get().GetUserId();
            return UpsertBetIfValid(command, userId);
        }

        private static void SetValues(Bet bet, ParticipantBetCommand command, Guid userId)
        {
            bet.BetResult = new Result { ScoreTeamA = command.ScoreTeamA, ScoreTeamB = command.ScoreTeamB };
            bet.GameId = command.GameId;
            bet.UserId = userId;
            bet.LastChanged = DateTimeOffset.UtcNow;
        }

        private async Task UpsertBetIfValid(ParticipantBetCommand command, Guid userId)
        {
            GameMetadata gameMetadata = await _gameMetadataCommandRepository.GetAsync(command.GameId);
            if (gameMetadata.StartDate < DateTimeOffset.UtcNow)
            {
                throw new ValidationException("Must no bet for a game which is already running.");
            }

            Bet bet = await _repository.GetByGameIdAndUserIdAsync(command.GameId, userId);
            if (bet != null)
            {
                if (bet.ActualResultPresent)
                {
                    throw new ValidationException("A bet which has an actual result can not be changed anymore.");
                }

                await _repository.UpdateAsync(bet.Id, b => SetValues(b, command, userId));
            }
            else
            {
                await _repository.AddAsync(b => SetValues(b, command, userId));
            }
        }
    }
}
