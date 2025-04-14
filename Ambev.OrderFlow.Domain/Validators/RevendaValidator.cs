using Ambev.OrderFlow.Domain.Aggregates;
using FluentValidation;

namespace Ambev.OrderFlow.Domain.Validators
{
    public class RevendaValidator : AbstractValidator<Revenda>
    {
        public RevendaValidator() 
        {
            RuleFor(c => c.NomeFantasia).NotEmpty();
            RuleFor(c => c.RazaoSocial).NotEmpty();
            RuleFor(c => c.Cnpj)
                .NotEmpty()
                .Matches(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})");
            RuleFor(c => c.Email)
                .NotEmpty()
                .Matches(@"/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/gi");
        }
    }
}
