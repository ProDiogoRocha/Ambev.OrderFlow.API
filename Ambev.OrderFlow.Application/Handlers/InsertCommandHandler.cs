using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class InsertCommandHandler<TDto, TEntity> : IRequestHandler<InsertCommand<TDto>, Guid>
    where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;   

        public InsertCommandHandler(IRepository<TEntity> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(InsertCommand<TDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(request.Dto);

                var property = typeof(TEntity).GetProperty("Id");
                var id = (Guid)(property?.GetValue(entity) ?? Guid.NewGuid());

                var response = await _repository.Add(entity);

                await _mediator.Publish(new InsertNotification<TDto>(id, request.Dto), cancellationToken);

                return response ? id : Guid.Empty;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification($"Erro ao tentar inserir {typeof(TEntity).Name}", ex.ToString()), cancellationToken);
                throw;
            }
        }
    }
}
