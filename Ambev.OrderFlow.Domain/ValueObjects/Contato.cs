using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.Bases;
using Ambev.OrderFlow.Domain.Validators;

namespace Ambev.OrderFlow.Domain.ValueObjects
{
    public class Contato : EntidadeBase<Contato>
    {
        public Guid Id { get; set; }
        public virtual Guid RevendaId { get; set; }
        public virtual Revenda Revenda { get; set; }
        public string Nome { get; private set; }
        public bool Principal { get; private set; }

        public Contato(string nome, bool principal)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Principal = principal;
            Validar(new ContatoValidator());
        }
    }
}
