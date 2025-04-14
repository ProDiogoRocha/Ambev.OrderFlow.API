using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class DeleteCommand<T> : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteCommand(Guid id)
        {
            Id = id;
        }
    }
}
