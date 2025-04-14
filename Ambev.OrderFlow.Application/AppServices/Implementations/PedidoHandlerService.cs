using Ambev.OrderFlow.Application.AppServices.Abstraction;
using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Entities;
using MediatR;

namespace Ambev.OrderFlow.Application.AppServices.Implementations
{
    public class PedidoHandlerService : BaseHandlerService<Pedido, PedidoDTO>, IHandlerService<Pedido, PedidoDTO>
    {
        public PedidoHandlerService(IMediator mediator) : base(mediator) { }
    }
}
