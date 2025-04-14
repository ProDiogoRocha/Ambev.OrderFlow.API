using Ambev.OrderFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ambev.OrderFlow.Infrastructure.Data.Factories
{
    public class RevendaContextFactory : IDesignTimeDbContextFactory<RevendaContext>
    {
        public RevendaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RevendaContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=RevendaPedidosDb;User Id=sa;Password=Passw0rd;TrustServerCertificate=True");

            return new RevendaContext(optionsBuilder.Options);
        }
    }
}
