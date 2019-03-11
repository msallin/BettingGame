using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Shared.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Betting.Core.Features.GameHandling
{
    internal class SetActualResultCommandHandler : ISubscriber
    {
        private readonly IBetCommandRepository _betCommandRepository;

        public SetActualResultCommandHandler(IBetCommandRepository betCommandRepository)
        {
            _betCommandRepository = betCommandRepository;
        }

        [Subscribe]
        public Task ExecuteAsync(SetActualResultCommand command)
        {
            return _betCommandRepository.SetActualResultForGame(command.GameId, new Result { ScoreTeamA = command.ScoreTeamA, ScoreTeamB = command.ScoreTeamB });
        }
    }
}
