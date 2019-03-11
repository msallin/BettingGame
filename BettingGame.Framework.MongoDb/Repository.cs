using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using MongoDbGenericRepository.Models;

using MongoDB.Driver;

namespace BettingGame.Framework.MongoDb
{
    public abstract class Repository<TCollection, TDomainEntity>
        where TCollection : IDocument, TDomainEntity, new()
    {
        protected Repository(DbContextFactory dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
        }

        protected DbContextFactory DbContextFactory { get; }

        public virtual async Task<TDomainEntity> AddAsync(Action<TDomainEntity> setValues)
        {
            DbContext dbContext = DbContextFactory.Create();

            var record = new TCollection { Id = Guid.NewGuid() };
            setValues(record);
            await BeforeInsert(record, dbContext);
            await dbContext.GetCollection<TCollection>().InsertOneAsync(record);
            return record;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var filter = new ExpressionFilterDefinition<TCollection>(collection => collection.Id == id);
            DeleteResult result = await DbContextFactory.Create().GetCollection<TCollection>().DeleteOneAsync(filter);
            if (result.DeletedCount < 0)
            {
                throw new ValidationException($"No record with id {id} found.");
            }
        }

        public virtual async Task<TDomainEntity> GetAsync(Guid id)
        {
            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<TCollection> filter = new ExpressionFilterDefinition<TCollection>(u => u.Id == id);
            TCollection entity = await dbContext.GetCollection<TCollection>().Find(filter).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<TDomainEntity> UpdateAsync(Guid id, Action<TDomainEntity> setValues)
        {
            DbContext dbContext = DbContextFactory.Create();
            FilterDefinition<TCollection> filter = new ExpressionFilterDefinition<TCollection>(collection => collection.Id == id);
            TCollection documentToUpdate = await dbContext.GetCollection<TCollection>().Find(filter).SingleOrDefaultAsync();
            if (documentToUpdate == null)
            {
                throw new ValidationException($"No record with id {id} found.");
            }

            setValues(documentToUpdate);
            await BeforeUpdate(documentToUpdate, dbContext);
            await dbContext.GetCollection<TCollection>().ReplaceOneAsync(filter, documentToUpdate);

            return documentToUpdate;
        }

        protected virtual Task BeforeInsert(TCollection record, DbContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task BeforeUpdate(TCollection record, DbContext context)
        {
            return Task.CompletedTask;
        }
    }
}
