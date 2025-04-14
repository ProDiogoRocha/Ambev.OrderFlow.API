using Ambev.OrderFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ambev.OrderFlow.Infrastructure.Data.Factories
{
    public class PedidoContextFactory : IDesignTimeDbContextFactory<PedidoContext>
    {
        public PedidoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PedidoContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=RevendaPedidosDb;User Id=sa;Password=Passw0rd;TrustServerCertificate=True");

            return new PedidoContext(optionsBuilder.Options);
        }
    }
}
