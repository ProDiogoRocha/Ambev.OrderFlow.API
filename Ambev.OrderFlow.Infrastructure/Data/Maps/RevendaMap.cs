using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.OrderFlow.Infrastructure.Data.Maps
{
    internal class RevendaMap : IEntityTypeConfiguration<Revenda>
    {
        public void Configure(EntityTypeBuilder<Revenda> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Cnpj).IsRequired().HasMaxLength(20);
            builder.Property(r => r.RazaoSocial).IsRequired();
            builder.Property(r => r.NomeFantasia).IsRequired();
            builder.Property(r => r.Email).IsRequired();

            builder.HasMany(r => r.Telefones)
                .WithOne(t=>t.Revenda)
                .HasForeignKey(t => t.RevendaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.EnderecosEntrega)
                .WithOne(e => e.Revenda)
                .HasForeignKey(e => e.RevendaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Contatos)
                .WithOne(c => c.Revenda)
                .HasForeignKey(c => c.RevendaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
