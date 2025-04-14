using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.Handlers;
using Ambev.OrderFlow.Application.Notifications;
using Ambev.OrderFlow.Domain.Entities;
using Ambev.OrderFlow.Domain.Interfaces;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Test.Unit.Handlers
{
    public class DeleteCommandHandlerTests
    {
        private readonly Mock<IRepository<Pedido>> _repositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly DeleteCommandHandler<Pedido> _handler;

        public DeleteCommandHandlerTests()
        {
            _repositoryMock = new Mock<IRepository<Pedido>>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new DeleteCommandHandler<Pedido>(_repositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Delete_Entity_And_Publish_Notification()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteCommand<Pedido>(id);
            _repositoryMock.Setup(r => r.Delete(It.IsAny<Expression<Func<Pedido, bool>>>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mediatorMock.Verify(m => m.Publish(It.Is<DeleteNotification<Pedido>>(n => n.Id == id), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.Delete(It.IsAny<Expression<Func<Pedido, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Publish_ErrorNotification_If_Exception_Occurs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteCommand<Pedido>(id);
            var exception = new Exception("Test error");

            _repositoryMock
                .Setup(r => r.Delete(It.IsAny<Expression<Func<Pedido, bool>>>()))
                .ThrowsAsync(exception);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Test error", ex.Message);

            _mediatorMock.Verify(m => m.Publish(It.Is<DeleteNotification<Pedido>>(n => n.Id == id), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.Is<ErrorNotification>(e =>
                e.Message.Contains("Erro ao tentar excluir") &&
                e.StackTrace.Contains("Test error")
            ), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
