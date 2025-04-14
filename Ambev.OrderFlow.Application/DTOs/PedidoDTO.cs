namespace Ambev.OrderFlow.Application.DTOs
{
    public class PedidoDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RevendaId { get; private set; }
        public Guid ClienteId { get; private set; } = default!;
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
        public List<ItemPedidoDTO> Itens { get; private set; } = new();
        public bool EmitidoParaAmbev { get; private set; }
    }
}
