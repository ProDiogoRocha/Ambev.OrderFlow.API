namespace Ambev.OrderFlow.Application.Messages
{
    public class PedidoAmbevMessage
    {
        public Guid PedidoId { get; set; }
        public Guid RevendaId { get; set; }
        public List<Item> Itens { get; set; }

        public class Item
        {
            public string Produto { get; set; }
            public int Quantidade { get; set; }
        }
    }
}
