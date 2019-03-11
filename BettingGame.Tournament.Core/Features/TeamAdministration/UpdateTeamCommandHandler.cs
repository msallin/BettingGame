using System.Threading.Tasks;

using BettingGame.Tournament.Core.Features.TeamAdministration.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.TeamAdministration
{
    internal class UpdateTeamCommandHandler : ISubscriber
    {
        private readonly ITeamCommandRepository _teamCommandRepository;

        public UpdateTeamCommandHandler(ITeamCommandRepository teamCommandRepository)
        {
            _teamCommandRepository = teamCommandRepository;
        }

        [Subscribe]
        public Task ExecuteAsync(UpdateTeamCommand command)
        {
            return _teamCommandRepository.UpdateAsync(command.Id, team =>
            {
                team.Group = command.Group;
                team.Name = command.Name;
                team.Iso2 = command.Iso2;
            });
        }
    }
}
