using Ambev.OrderFlow.Application.AppServices.Abstraction;
using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Aggregates;
using MediatR;

namespace Ambev.OrderFlow.Application.AppServices.Implementations
{
    public class RevendaHandlerService : BaseHandlerService<Revenda, RevendaDTO>, IEmissorHandlerService
    {
        public RevendaHandlerService(IMediator mediator) : base(mediator) { }

        public async Task<IEnumerable<Guid>> EmitirPedidosRevenda(Guid idRevenda)
        {
            EmitirPedidosRevendaCommand emitirPedidosRevendaCommand = new EmitirPedidosRevendaCommand(idRevenda);
            return await _mediator.Send(emitirPedidosRevendaCommand);
        }
    }
}
