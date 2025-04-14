using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class InsertNotification<TDto> : INotification
    {
        public Guid Id { get; }
        public TDto Dto { get; }

        public InsertNotification(Guid id, TDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
