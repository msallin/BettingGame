using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Domain;
using BettingGame.Betting.Core.Shared.Abstraction;

namespace BettingGame.Betting.Core.Features.GameHandling.Abstraction
{
    public interface IGameMetadataCommandRepository : ICommandRepository<GameMetadata>
    {
        Task<GameMetadata> UpsertAsync(Guid id, Action<GameMetadata> setValues);

        Task<IEnumerable<GameMetadata>> GetByDateAsync(DateTimeOffset date);
    }
}
