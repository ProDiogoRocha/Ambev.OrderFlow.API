using Ambev.OrderFlow.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<T> : IDisposable, IRepository<T> where T : class
    {
        public DbContext _dbContext;
        public DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = _dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual Task<IQueryable<T>> QueryAsync()
        {
            return Task.FromResult( _dbSet.AsQueryable());
        }

        public virtual async Task<bool> Add(T entity)
        {
            _dbSet.Add(entity);
            return await _dbContext.SaveChangesAsync() == 1 ? true : false;
        }

        public virtual async Task<bool> Delete(Expression<Func<T, bool>> expression)
        {
            T t = await _dbSet.FirstAsync(expression);
            _dbSet.Remove(t);
            return await _dbContext.SaveChangesAsync() == 1 ? true : false;
        }

        public async void Dispose()
        {
            await _dbContext.DisposeAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, object>> orderBy, bool asc = true)
        {
            return asc ? await _dbSet.OrderBy(orderBy).ToListAsync() : await _dbSet.OrderByDescending(orderBy).ToListAsync();
        }

        public virtual async Task<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstAsync(expression);
        }

        public virtual async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asc = true)
        {
            return asc ? await _dbSet.Where(expression).OrderBy(orderBy).ToListAsync() : await _dbSet.Where(expression).OrderByDescending(orderBy).ToListAsync();
        }

        public virtual async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            return await _dbContext.SaveChangesAsync() == 1 ? true : false;
        }
    }
}
