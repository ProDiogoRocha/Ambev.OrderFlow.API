using Ambev.OrderFlow.Domain.Bases;
using Ambev.OrderFlow.Domain.Validators;

namespace Ambev.OrderFlow.Domain.Entities
{
    public class Pedido : EntidadeBase<Pedido>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RevendaId { get; private set; }
        public Guid ClienteId { get; private set; } = default!;
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
        public List<ItemPedido> Itens { get; private set; } = new();
        public bool EmitidoParaAmbev { get; set; }


        protected Pedido() { }

        public Pedido(Guid revendaId, Guid clienteId, DateTime dataCriacao, bool emitidoParaAmbev = false)
        {
            Id = Guid.NewGuid();
            SetPedido(revendaId, clienteId, dataCriacao, emitidoParaAmbev);
        }

        public void SetPedido(Guid revendaId, Guid clienteId, DateTime dataCriacao, bool emitidoParaAmbev)
        {
            RevendaId = revendaId;
            ClienteId = clienteId;
            DataCriacao = dataCriacao;
            EmitidoParaAmbev = emitidoParaAmbev;

            Validar(new PedidoValidator());
        }

        public void AddItens(ItemPedido itemPedido)
        {
            Itens.Add(itemPedido);
        }

        public int TotalUnidades() => Itens.Sum(i => i.Quantidade);
    }
}
