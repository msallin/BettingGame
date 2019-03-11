using System;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Shared.Abstraction;

namespace BettingGame.Betting.Core.Features.TeamHandling.Abstraction
{
    public interface ITeamMetadataCommandRepository : ICommandRepository<TeamMetadata>
    {
        Task<TeamMetadata> UpsertAsync(Guid id, Action<TeamMetadata> setValues);
    }
}
