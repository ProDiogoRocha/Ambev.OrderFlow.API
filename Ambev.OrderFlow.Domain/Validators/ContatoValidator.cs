using Ambev.OrderFlow.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.OrderFlow.Domain.Validators
{
    public class ContatoValidator : AbstractValidator<Contato>
    {
        public ContatoValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do contato é obrigatório.");
        }
    }
}
