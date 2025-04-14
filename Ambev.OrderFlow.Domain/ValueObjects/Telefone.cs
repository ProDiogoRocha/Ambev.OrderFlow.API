using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.Bases;
using Ambev.OrderFlow.Domain.Validators;

namespace Ambev.OrderFlow.Domain.ValueObjects
{
    public class Telefone : EntidadeBase<Telefone>
    {
        public Guid Id { get; set; }
        public virtual Guid RevendaId { get; set; }
        public virtual Revenda Revenda { get; set; }
        public string Numero { get; private set; }

        public Telefone(string numero)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            Validar(new TelefoneValidator());
        }
    }
}
