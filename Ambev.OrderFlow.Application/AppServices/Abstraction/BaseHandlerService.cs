using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.Commands;
using MediatR;

namespace Ambev.OrderFlow.Application.AppServices.Abstraction
{
    public abstract class BaseHandlerService<TEntity, TDto> : IHandlerService<TEntity, TDto>
    {
        public readonly IMediator _mediator;

        protected BaseHandlerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual async Task<TDto> GetById(Guid id)
        {
            SelectOneCommand<TEntity, TDto> selectOneCommand = new SelectOneCommand<TEntity, TDto>(id);
            return await _mediator.Send(selectOneCommand);
        }

        public virtual async Task<IEnumerable<TDto>> GetByFilter(SelectByFilterCommand<TEntity, TDto> selectCommand)
        {
            return await _mediator.Send(selectCommand);
        }

        public virtual async Task<IEnumerable<TDto>> GetAll(string orderBy)
        {
            SelectAllCommand<TEntity, TDto> selectCommand = new SelectAllCommand<TEntity, TDto>(orderBy);
            return await _mediator.Send(selectCommand);
        }

        public virtual async Task<Guid> Insert(InsertCommand<TDto> insertCommand)
        {
            return await _mediator.Send(insertCommand);
        }

        public virtual async Task<bool> Update(UpdateCommand<TDto> updateCommand)
        {
            return await _mediator.Send(updateCommand);
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            DeleteCommand<TDto> deleteCommand = new DeleteCommand<TDto>(id);
            return await _mediator.Send(deleteCommand);
        }
    }
}
