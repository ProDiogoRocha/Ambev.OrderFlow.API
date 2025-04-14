using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Messages;
using Ambev.OrderFlow.Application.Messaging;
using Ambev.OrderFlow.Domain.Entities;
using Ambev.OrderFlow.Domain.Interfaces;
using MediatR;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class EmitirPedidoCommandHandler(IPedidoRepository pedidoRepository, IMessageBus bus) : IRequestHandler<EmitirPedidosRevendaCommand, IEnumerable<Guid>>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IMessageBus _bus = bus;

        public async Task<IEnumerable<Guid>> Handle(EmitirPedidosRevendaCommand request, CancellationToken cancellationToken)
        {
            List<Pedido> pedidos = null;

            pedidos = (await _pedidoRepository.GetBy(pr => pr.RevendaId == request.RevendaId && pr.EmitidoParaAmbev == false, pr => pr.DataCriacao)).ToList();

            if (pedidos == null)
                throw new Exception("Pedidos não encontrados");

            List<PedidoAmbevMessage> listaParaEmissão = new List<PedidoAmbevMessage>();

            foreach (var pedido in pedidos)
            {
                if (pedido.TotalUnidades() >= 1000)
                    listaParaEmissão.Add(
                   new PedidoAmbevMessage
                   {
                       PedidoId = pedido.Id,
                       RevendaId = pedido.RevendaId,
                       Itens = pedido.Itens.Select(i => new PedidoAmbevMessage.Item
                       {
                           Produto = i.Produto,
                           Quantidade = i.Quantidade
                       }).ToList()
                   });
            }

            foreach (var message in listaParaEmissão)
            {
                try
                {
                    await _bus.PublicarAsync(message);
                    var pedido = pedidos.Where(p => p.Id == message.PedidoId).Select(p => p).FirstOrDefault();
                    pedido.EmitidoParaAmbev = true;

                    await _pedidoRepository.Update(pedido);
                }
                catch
                {
                    await _bus.PublicarAsync(new PedidoRevendaRegistradoEvent(message.PedidoId));
                    continue;
                }
            }
            return pedidos.Where(p => p.EmitidoParaAmbev).Select(p => p.Id);
        }
    }
}
