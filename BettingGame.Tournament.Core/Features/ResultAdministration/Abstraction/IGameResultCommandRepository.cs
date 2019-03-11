using System;
using System.Threading.Tasks;

using BettingGame.Tournament.Core.Domain;

namespace BettingGame.Tournament.Core.Features.ResultAdministration.Abstraction
{
    public interface IGameResultCommandRepository
    {
        Task UpsertAsync(Guid gameId, Result result);
    }
}
