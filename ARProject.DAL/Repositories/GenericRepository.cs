using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>  where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public GenericRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetByFilterAsync(FilterDefinition<T> filter) =>
            await _collection.Find(filter).FirstOrDefaultAsync();

        public async Task AddAsync(T entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task<bool> ExistsAsync(FilterDefinition<T> filter) =>
            await _collection.Find(filter).AnyAsync();
    }
}
