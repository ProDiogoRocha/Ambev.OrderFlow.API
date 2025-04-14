using Ambev.OrderFlow.Domain.Entities;
using FluentValidation;

namespace Ambev.OrderFlow.Domain.Validators
{
    public class ItemPedidoValidator : AbstractValidator<ItemPedido>
    {
        public ItemPedidoValidator()
        {
            RuleFor(p => p.Produto)
                .NotEmpty().WithMessage("O descrição do produto é obrigatório");
            RuleFor(c => c.Quantidade)
                .NotEmpty().WithMessage("A quantidade do produto é obrigatório.");
        }
    }
}
