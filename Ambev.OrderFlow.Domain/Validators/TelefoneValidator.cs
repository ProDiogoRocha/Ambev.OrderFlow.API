using Ambev.OrderFlow.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.OrderFlow.Domain.Validators
{
    public class TelefoneValidator : AbstractValidator<Telefone>
    {
        public TelefoneValidator()
        {
            RuleFor(t => t.Numero)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter 10 ou 11 dígitos numéricos.");
        }
    }
}
