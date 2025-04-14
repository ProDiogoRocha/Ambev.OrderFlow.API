using System.Linq.Expressions;

namespace Ambev.OrderFlow.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> QueryAsync();
        Task<T> GetBy(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> expression, Expression<Func<T,object>> orderBy, bool asc = true);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, object>> orderBy, bool asc = true);
        Task<bool> Update(T entity);
        Task<bool> Delete(Expression<Func<T, bool>> expression);
        Task<bool> Add(T entity);
    }
}
