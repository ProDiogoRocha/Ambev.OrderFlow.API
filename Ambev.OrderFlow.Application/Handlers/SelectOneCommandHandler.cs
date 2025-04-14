using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class SelectOneCommandHandler<TEntity, TDto> : IRequestHandler<SelectOneCommand<TEntity, TDto>, TDto?>
    where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SelectOneCommandHandler(IRepository<TEntity> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TDto?> Handle(SelectOneCommand<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var predicate = BuildPredicateById<TEntity>(request.Id);
                var entity = await _repository.GetBy(predicate);

                if (entity is null)
                    return default;

                await _mediator.Publish(new SelectOneNotification<TDto>(request.Id), cancellationToken);

                return _mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification($"Erro ao tentar selecionar os dados de {typeof(TEntity).Name}", ex.ToString()), cancellationToken);
                throw;
            }
        }

        private static Expression<Func<TEntity, bool>> BuildPredicateById<TEntity>(Guid id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(parameter, "Id");
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
        }
    }
}
