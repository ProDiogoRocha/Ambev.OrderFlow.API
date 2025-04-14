using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class SelectOneNotification<TDto> : INotification
    {
        public Guid Id { get; }

        public SelectOneNotification(Guid id)
        {
            Id = id;
        }
    }
}
