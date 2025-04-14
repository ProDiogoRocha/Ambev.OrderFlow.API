using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class EmitirPedidosRevendaCommand : IRequest<IEnumerable<Guid>>
    {
        public Guid RevendaId { get; }

        public EmitirPedidosRevendaCommand(Guid revendaId)
        {
            RevendaId = revendaId;
        }
    }
}
