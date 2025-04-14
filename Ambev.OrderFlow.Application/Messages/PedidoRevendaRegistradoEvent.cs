namespace Ambev.OrderFlow.Application.Messages
{
    public class PedidoRevendaRegistradoEvent
    {
        public Guid PedidoId { get; set; }

        public PedidoRevendaRegistradoEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
