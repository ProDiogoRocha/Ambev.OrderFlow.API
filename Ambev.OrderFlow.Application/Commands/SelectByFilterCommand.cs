using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class SelectByFilterCommand<TEntity, TDto> : IRequest<List<TDto>>
    {
        public List<Dictionary<string, string>> Filters { get; }
        public string OrderBy { get; }

        public SelectByFilterCommand(List<Dictionary<string, string>> filters, string orderBy)
        {
            Filters = filters;
            OrderBy = orderBy;
        }
    }
}
