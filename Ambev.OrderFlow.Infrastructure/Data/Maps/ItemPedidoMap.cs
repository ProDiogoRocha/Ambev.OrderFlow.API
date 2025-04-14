using Ambev.OrderFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.OrderFlow.Infrastructure.Data.Maps
{
    public class ItemPedidoMap : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
