using FluentValidation;

namespace Ambev.OrderFlow.Domain.Bases
{
    public abstract class EntidadeBase<T> where T : class
    {
        protected void Validar(AbstractValidator<T> validator)
        {
            var result = validator.Validate(this as T);

            if (!result.IsValid)
            {
                var erros = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(erros);
            }
        }
    }
}
