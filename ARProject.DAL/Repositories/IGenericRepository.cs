using MongoDB.Driver;

namespace ARProject.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByFilterAsync(FilterDefinition<T> filter);
        Task AddAsync(T entity);
        Task<bool> ExistsAsync(FilterDefinition<T> filter);
    }
}
