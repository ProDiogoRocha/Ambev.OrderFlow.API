using Ambev.OrderFlow.Domain.Bases;
using Ambev.OrderFlow.Domain.Validators;
using Ambev.OrderFlow.Domain.ValueObjects;

namespace Ambev.OrderFlow.Domain.Aggregates
{
    public class Revenda : EntidadeBase<Revenda>
    {
        public Guid Id { get; private set; }
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Email { get; private set; }
        public virtual List<Telefone> Telefones { get; private set; } = new();
        public virtual List<Contato> Contatos { get; private set; } = new();
        public virtual List<Endereco> EnderecosEntrega { get; private set; } = new();

        protected Revenda() { }

        public Revenda(string cnpj, string razaoSocial, string nomeFantasia, string email)
        {
            Id = Guid.NewGuid();
            SetRevenda(cnpj, razaoSocial, nomeFantasia, email);
        }

        public void SetRevenda(string cnpj, string razaoSocial, string nomeFantasia, string email)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Email = email;

            Validar(new RevendaValidator());
        }

        public void AddTelefone(IEnumerable<Telefone> telefones) => Telefones.AddRange(telefones);
        public void AddContato(IEnumerable<Contato> contatos) => Contatos.AddRange(contatos);
        public void AddEndereco(IEnumerable<Endereco> enderecos) => EnderecosEntrega.AddRange(enderecos);
    }
}
