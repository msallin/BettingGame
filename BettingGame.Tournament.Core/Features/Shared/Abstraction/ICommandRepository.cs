﻿using System;
using System.Threading.Tasks;

namespace BettingGame.Tournament.Core.Features.Shared.Abstraction
{
    public interface ICommandRepository<TEntity>
    {
        Task<TEntity> AddAsync(Action<TEntity> setValues);

        Task DeleteAsync(Guid id);

        Task<TEntity> GetAsync(Guid id);

        Task<TEntity> UpdateAsync(Guid id, Action<TEntity> setValues);
    }
}
