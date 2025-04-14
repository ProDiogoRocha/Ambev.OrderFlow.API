using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class SelectAllCommand<TEntity, TDto> : IRequest<List<TDto>>
    {
        public string OrderBy { get; }

        public SelectAllCommand(string orderBy)
        {
            OrderBy = orderBy;
        }
    }
}
