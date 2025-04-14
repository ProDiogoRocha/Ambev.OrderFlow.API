using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class UpdateNotification<TDto> : INotification
    {
        public TDto Dto { get; }

        public UpdateNotification(TDto dto)
        {
            Dto = dto;
        }
    }
}
