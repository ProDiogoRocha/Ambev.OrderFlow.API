using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class DeleteNotification<TDto> : INotification
    {
        public Guid Id { get; }

        public DeleteNotification(Guid id)
        {
            Id = id;
        }
    }
}
