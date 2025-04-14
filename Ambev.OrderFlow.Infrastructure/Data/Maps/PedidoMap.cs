using Ambev.OrderFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.OrderFlow.Infrastructure.Data.Maps
{
    internal class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.RevendaId).IsRequired();
            builder.Property(p => p.DataCriacao).IsRequired();

            builder.HasMany(p => p.Itens)
                .WithOne(ip => ip.Pedido)
                .HasForeignKey(t => t.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
