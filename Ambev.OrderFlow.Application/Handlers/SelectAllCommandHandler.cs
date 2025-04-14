using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class SelectAllCommandHandler<TEntity, TDto> : IRequestHandler<SelectAllCommand<TEntity, TDto>, List<TDto>>
     where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SelectAllCommandHandler(IRepository<TEntity> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<TDto>> Handle(SelectAllCommand<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _repository.QueryAsync();

                query = OrderByProperty(query, request.OrderBy);

                var dtos = query
                    .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .ToList();

                await _mediator.Publish(new SelectAllNotification<TDto>(request.OrderBy, dtos), cancellationToken);

                return dtos;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification($"Erro ao tentar selecionar todas os dados de {typeof(TEntity).Name}", ex.ToString()), cancellationToken);
                throw;
            }
        }

        private static IQueryable<TEntity> OrderByProperty(IQueryable<TEntity> query, string propertyName)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(param, propertyName);
            var lambda = Expression.Lambda(property, param);

            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TEntity), property.Type);

            return (IQueryable<TEntity>)method.Invoke(null, new object[] { query, lambda })!;
        }
    }
}
