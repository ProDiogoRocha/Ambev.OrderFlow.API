using MediatR;

namespace Ambev.OrderFlow.Application.Commands
{
    public class UpdateCommand<TDto> : IRequest<bool>
    {
        public TDto Dto { get; }

        public UpdateCommand(TDto dto)
        {
            Dto = dto;
        }
    }
}
