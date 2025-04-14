using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class UpdateCommandHandler<TDto, TEntity> : IRequestHandler<UpdateCommand<TDto>, bool>
    where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateCommandHandler(IRepository<TEntity> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateCommand<TDto> request, CancellationToken cancellationToken)
        {
            try
            {
                TEntity? entity = (TEntity?)Activator.CreateInstance(typeof(TEntity));

                _mapper.Map(request.Dto, entity);

                await _mediator.Publish(new UpdateNotification<TDto>(request.Dto), cancellationToken);

                return await _repository.Update(entity);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification($"Erro ao tentar atualizar os dados de {typeof(TEntity).Name}", ex.ToString()), cancellationToken);
                throw;
            }
        }
    }
}
