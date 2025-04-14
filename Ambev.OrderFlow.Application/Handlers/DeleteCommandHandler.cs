using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.ExtensionsMethods;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Interfaces;
using MediatR;

namespace Ambev.OrderFlow.Application.Handlers
{
    public class DeleteCommandHandler<T> : IRequestHandler<DeleteCommand<T>, bool>
    where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly IMediator _mediator;

        public DeleteCommandHandler(IRepository<T> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            try
            {
                var predicate = MediatorExtensionsMethods.BuildPredicateById<T>(request.Id);

                await _mediator.Publish(new DeleteNotification<T>(request.Id), cancellationToken);

                return await _repository.Delete(predicate);
            }
            catch (Exception ex) 
            {
                await _mediator.Publish(new ErrorNotification($"Erro ao tentar excluir {typeof(T).Name}", ex.ToString()),cancellationToken);
                throw;
            }
        }
    }
}
