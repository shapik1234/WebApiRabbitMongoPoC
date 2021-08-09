using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Database;
using CustomerApi.Data.Entities;
using MongoDB.Driver;

namespace CustomerApi.Data.Repository.v1
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> 
        where TEntity : Entities.MongoIdentity, new()
    {
        protected readonly IMongoContext<TEntity> mongoContext;
        private IMongoCollection<TEntity> collection;
        public MongoRepository(IMongoContext<TEntity> context)
        {
            mongoContext = context;
            collection = context.GetCollection<TEntity>();
        }       

        public async Task<List<TEntity>> Get(CancellationToken cancellationToken = default)
        {
            var all = await collection.FindAsync(Builders<TEntity>.Filter.Empty, null, cancellationToken);
            return await all.ToListAsync();
        }

        public async Task<TEntity> GetById(string id, CancellationToken cancellationToken = default)
        {
            var result = await collection.Aggregate().Match(x => x.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
            return result;
        }       

        public async Task Create(TEntity obj, CancellationToken cancellationToken = default)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }

            await collection.InsertOneAsync(obj, null, cancellationToken);
        }

        public async virtual void Update(TEntity obj, CancellationToken cancellationToken = default)
        {
            await collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(i => i.Id, obj.Id), obj);
        }

        public async void Delete(string id, CancellationToken cancellationToken = default)
        {
            await collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq(i => i.Id, id), cancellationToken);
        }
        public async Task<IList<TEntity>> SearchFor(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var result = await collection.Aggregate().Match(predicate).ToListAsync();
            return result;
        }
    }
}