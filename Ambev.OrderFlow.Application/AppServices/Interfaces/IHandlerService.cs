using Ambev.OrderFlow.Application.Commands;

namespace Ambev.OrderFlow.Application.AppServices.Interfaces
{
    public interface IHandlerService<TEntity,TDto>
    {
        Task<TDto> GetById(Guid id);
        Task<IEnumerable<TDto>> GetByFilter(SelectByFilterCommand<TEntity, TDto> selectCommand);
        Task<IEnumerable<TDto>> GetAll(string orderBy);
        Task<Guid> Insert(InsertCommand<TDto> insertCommand);
        Task<bool> Update(UpdateCommand<TDto> updateCommand);
        Task<bool> Delete(Guid id);
    }
}
