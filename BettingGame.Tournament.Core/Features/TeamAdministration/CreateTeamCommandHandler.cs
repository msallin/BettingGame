using System.Threading.Tasks;

using BettingGame.DomainEvents;
using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.TeamAdministration.Abstraction;

using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace BettingGame.Tournament.Core.Features.TeamAdministration
{
    internal class CreateTeamCommandHandler : ISubscriber
    {
        private readonly ITeamCommandRepository _teamCommandRepository;

        private readonly IEventPublisher _publisher;

        public CreateTeamCommandHandler(ITeamCommandRepository teamCommandRepository, IEventPublisher publisher)
        {
            _teamCommandRepository = teamCommandRepository;
            _publisher = publisher;
        }

        [Subscribe]
        public async Task ExecuteAsync(CreateTeamCommand command)
        {
            Team createdTeam = await _teamCommandRepository.AddAsync(team =>
            {
                team.Group = command.Group;
                team.Name = command.Name;
                team.Iso2 = command.Iso2;
                team.FifaCode = command.FifaCode;
            });

            await _publisher.PublishAsync(new TeamChangedEvent { Id = createdTeam.Id, FifaCode = createdTeam.FifaCode });
        }
    }
}
