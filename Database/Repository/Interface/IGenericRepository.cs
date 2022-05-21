using System.Linq.Expressions;

namespace Database.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public Task<IEnumerable<T>> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllAsNoTrackingAsync();
        public Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entities);
        public void Update(T entity);
        public void UpdateRange(IEnumerable<T> entities);
        public Task SaveChangesAsync();


    }
}