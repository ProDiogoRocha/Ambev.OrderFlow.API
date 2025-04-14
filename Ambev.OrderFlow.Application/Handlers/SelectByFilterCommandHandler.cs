using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class SelectByFilterCommandHandler<TEntity, TDto> : IRequestHandler<SelectByFilterCommand<TEntity, TDto>, List<TDto>>
    where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SelectByFilterCommandHandler(IRepository<TEntity> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<TDto>> Handle(SelectByFilterCommand<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var query = (await _repository.QueryAsync()).AsQueryable();

                foreach (var filterDict in request.Filters)
                {
                    foreach (var kvp in filterDict)
                    {
                        query = ApplyFilter(query, kvp.Key, kvp.Value);
                    }
                }

                query = OrderByProperty(query, request.OrderBy);
                var list = query.ToList();

                var dtoList = list.Select(_mapper.Map<TDto>).ToList();

                await _mediator.Send(new SelectByFilterNotification<TDto>(request.Filters, request.OrderBy, dtoList));

                return dtoList;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification($"Erro ao tentar selecionar os dados filtrados de {typeof(TEntity).Name}", ex.ToString()), cancellationToken);
                throw;
            }
        }

        private static IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, string propertyName, string value)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(param, propertyName);

            var constant = Expression.Constant(Convert.ChangeType(value, property.Type));

            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, param);

            return query.Where(lambda);
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
