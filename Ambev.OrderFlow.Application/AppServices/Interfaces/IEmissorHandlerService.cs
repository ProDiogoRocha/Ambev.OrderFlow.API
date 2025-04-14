using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Aggregates;

namespace Ambev.OrderFlow.Application.AppServices.Interfaces
{
    public interface IEmissorHandlerService : IHandlerService<Revenda, RevendaDTO>
    {
        Task<IEnumerable<Guid>> EmitirPedidosRevenda(Guid idRevenda);
    }
}
