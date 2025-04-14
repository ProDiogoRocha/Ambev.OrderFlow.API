namespace Ambev.OrderFlow.Application.DTOs
{
    public class EnderecoDTO
    {
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Cep { get; private set; }
        public bool Principal { get; private set; }
    }
}
