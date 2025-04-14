using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.Bases;
using Ambev.OrderFlow.Domain.Validators;

namespace Ambev.OrderFlow.Domain.ValueObjects
{
    public class Endereco : EntidadeBase<Endereco>
    {
        public Guid Id { get; set; }
        public virtual Guid RevendaId { get; set; }
        public virtual Revenda Revenda { get; set; }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Cep { get; private set; }
        public bool Principal { get; private set; }

        public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep, bool principal = false)
        {
            Id = new Guid();
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Principal = principal;

            Validar(new EnderecoValidator());
            Principal = principal;
        }
    }
}
