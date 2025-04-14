using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class SelectOneCommand<TEntity, TDto> : IRequest<TDto>
    {
        public Guid Id { get; }

        public SelectOneCommand(Guid id)
        {
            Id = id;
        }
    }
}
