using Ambev.OrderFlow.Domain.Entities;
using Ambev.OrderFlow.Domain.Interfaces;
using Ambev.OrderFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Infrastructure.Data.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(PedidoContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Pedido> GetBy(Expression<Func<Pedido, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(r => r.Itens)
                .FirstAsync();
        }

        public async override Task<IEnumerable<Pedido>> GetBy(Expression<Func<Pedido, bool>> expression, Expression<Func<Pedido, object>> orderBy, bool asc = true)
        {
            return asc
                ?
                await _dbSet.Where(expression)
                .OrderBy(orderBy)
                .Include(r => r.Itens)
                .ToListAsync()
                :
                await _dbSet.Where(expression)
                .OrderByDescending(orderBy)
                .Include(r => r.Itens)
                .ToListAsync();
        }
    }
}
