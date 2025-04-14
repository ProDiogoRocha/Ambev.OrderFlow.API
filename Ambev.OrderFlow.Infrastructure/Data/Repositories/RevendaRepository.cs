using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.Interfaces;
using Ambev.OrderFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Infrastructure.Data.Repositories
{
    public class RevendaRepository : BaseRepository<Revenda>, IRevendaRepository
    {
        public RevendaRepository(RevendaContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Revenda> GetBy(Expression<Func<Revenda, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(r => r.Contatos)
                .Include(r => r.EnderecosEntrega)
                .Include(r => r.Telefones)
                .FirstAsync();
        }

        public async override Task<IEnumerable<Revenda>> GetBy(Expression<Func<Revenda, bool>> expression, Expression<Func<Revenda, object>> orderBy, bool asc = true)
        {
            return asc 
                ?
                await _dbSet.Where(expression)
                .OrderBy(orderBy)
                .Include(r => r.Contatos)
                .Include(r => r.EnderecosEntrega)
                .Include(r => r.Telefones)
                .ToListAsync()
                :
                await _dbSet.Where(expression)
                .OrderByDescending(orderBy)
                .Include(r => r.Contatos)
                .Include(r => r.EnderecosEntrega)
                .Include(r => r.Telefones)
                .ToListAsync();
        }
    }
}
