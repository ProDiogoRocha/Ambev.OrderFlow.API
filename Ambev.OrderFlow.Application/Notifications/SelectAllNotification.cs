using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class SelectAllNotification<TDto> : INotification
    {
        public string OrderBy { get; }
        public List<TDto> Data { get; }

        public SelectAllNotification(string orderBy, List<TDto> data)
        {
            OrderBy = orderBy;
            Data = data;
        }
    }
}
