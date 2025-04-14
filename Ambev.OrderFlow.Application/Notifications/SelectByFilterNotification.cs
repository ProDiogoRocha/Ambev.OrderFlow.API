using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class SelectByFilterNotification<TDto> : INotification
    {
        public List<Dictionary<string, string>> Filters { get; }
        public string OrderBy { get; }
        public List<TDto> Data { get; }

        public SelectByFilterNotification(List<Dictionary<string, string>> filters, string orderBy, List<TDto> data)
        {
            Filters = filters;
            OrderBy = orderBy;
            Data = data;
        }
    }
}
