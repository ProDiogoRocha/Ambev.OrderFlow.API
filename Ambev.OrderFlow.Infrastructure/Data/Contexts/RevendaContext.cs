using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Infrastructure.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace Ambev.OrderFlow.Infrastructure.Data.Contexts
{
    public class RevendaContext : DbContext
    {
        public RevendaContext(DbContextOptions<RevendaContext> options) : base(options)
        {
        }
        public DbSet<Revenda> Revendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RevendaMap());
            modelBuilder.ApplyConfiguration(new ContatoMap());
            modelBuilder.ApplyConfiguration(new TelefoneMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}
