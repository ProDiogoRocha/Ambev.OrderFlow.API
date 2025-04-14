using Ambev.OrderFlow.Domain.Bases;
using Ambev.OrderFlow.Domain.Validators;

namespace Ambev.OrderFlow.Domain.Entities
{
    public class ItemPedido : EntidadeBase<ItemPedido>
    {
        public Guid Id { get; set; }
        public virtual Guid PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public string Produto { get; set; } = default!;
        public int Quantidade { get; set; }

        ItemPedido() { }

        ItemPedido(Guid pedidoId, string produto, int quantidade)
        {
            SetPedido(pedidoId, produto, quantidade);
        }

        public void SetPedido(Guid pedidoId, string produto, int quantidade)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            Produto = produto;
            Quantidade = quantidade;

            Validar(new ItemPedidoValidator());
        }
    }
}
