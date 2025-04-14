using Ambev.OrderFlow.Domain.Entities;
using Ambev.OrderFlow.Infrastructure.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace Ambev.OrderFlow.Infrastructure.Data.Contexts
{
    public class PedidoContext : DbContext
    {
        public PedidoContext(DbContextOptions<PedidoContext> options) : base(options)
        {
        }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ItemPedidoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
