using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Database.Repository.Interface;
using Database.EFCore;
using System.Collections;

namespace Database.Repository.Implement.EF
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DemoContext _context;

        public GenericRepository(DemoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }
        public async Task<IEnumerable<T>> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}