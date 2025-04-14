using Ambev.OrderFlow.Domain.Entities;
using FluentValidation;

namespace Ambev.OrderFlow.Domain.Validators
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O Id do Pedido é obrigatório");
            RuleFor(c => c.RevendaId)
                .NotEmpty().WithMessage("O Id da Revenda é obrigatório.");
            RuleFor(c => c.ClienteId)
                .NotEmpty().WithMessage("O Identificador do Cliente é Obrigatório");
            RuleFor(p => p.DataCriacao)
                .NotEmpty().WithMessage("A DataCriacao é Obrigatório");
        }
    }
}
