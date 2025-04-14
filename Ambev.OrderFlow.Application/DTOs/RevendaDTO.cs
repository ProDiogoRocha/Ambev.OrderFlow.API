namespace Ambev.OrderFlow.Application.DTOs
{
    public class RevendaDTO
    {
        public Guid Id { get; private set; }
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Email { get; private set; }
        public List<TelefoneDTO> Telefones { get; private set; } = new();
        public List<ContatoDTO> Contatos { get; private set; } = new();
        public List<EnderecoDTO> EnderecosEntrega { get; private set; } = new();
    }
}
