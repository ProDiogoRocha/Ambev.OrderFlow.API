using Ambev.OrderFlow.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.OrderFlow.Domain.Validators
{
    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(e => e.Rua).NotEmpty();
            RuleFor(e => e.Bairro).NotEmpty();
            RuleFor(e => e.Numero).NotEmpty();
            RuleFor(e => e.Cidade).NotEmpty();
            RuleFor(e => e.Estado).NotEmpty();
            RuleFor(e => e.Cep)
                .NotEmpty()
                .Matches(@"^\d{5}-?\d{3}$").WithMessage("CEP inválido");
        }
    }
}
