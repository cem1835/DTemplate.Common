using DTemplate.Common.GenericRepo;
using DTemplate.Common.Helper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.MongoRepository
{
    class MongoDbRepository<TEntity> : IMongoRepository<TEntity> where TEntity : IEntity
    {
        protected IMongoCollection<TEntity> Collection { get; }

        public MongoDbRepository(IMongoDatabase database)
        {
            var collectionName = GetCollectionName<TEntity>();

            Collection = database.GetCollection<TEntity>(collectionName);
            
        }

        public async Task AddAsync(TEntity entity)
        {
            
            await Collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Collection.DeleteOneAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).AnyAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
             await Collection.ReplaceOneAsync(e => e.Id==entity.Id, entity);
        }

        private string GetCollectionName<TEntity>()
        {
            var nameAttribute = typeof(TEntity).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault();
            var collectionName = nameAttribute != null ? (nameAttribute as CollectionNameAttribute).TableName : nameof(TEntity);

            return collectionName;
        }
    }
}
