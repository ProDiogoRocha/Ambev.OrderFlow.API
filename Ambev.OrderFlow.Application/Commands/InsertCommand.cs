using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class InsertCommand<TDto> : IRequest<Guid>
    {
        public TDto Dto { get; }

        public InsertCommand(TDto dto)
        {
            Dto = dto;
        }
    }
}
