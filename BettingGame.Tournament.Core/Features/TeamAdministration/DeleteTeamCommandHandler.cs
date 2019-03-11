using System.Threading.Tasks;

using BettingGame.Tournament.Core.Features.TeamAdministration.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.TeamAdministration
{
    internal class DeleteTeamCommandHandler : ISubscriber
    {
        private readonly ITeamCommandRepository _teamCommandRepository;

        public DeleteTeamCommandHandler(ITeamCommandRepository teamCommandRepository)
        {
            _teamCommandRepository = teamCommandRepository;
        }

        [Subscribe]
        public Task ExecuteAsync(DeleteTeamCommand command)
        {
            return _teamCommandRepository.DeleteAsync(command.Id);
        }
    }
}
