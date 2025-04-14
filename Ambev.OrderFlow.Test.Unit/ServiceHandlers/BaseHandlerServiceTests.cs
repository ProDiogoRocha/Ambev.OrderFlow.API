using Ambev.OrderFlow.Application.AppServices.Abstraction;
using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Entities;
using MediatR;
using Moq;

namespace Ambev.OrderFlow.Test.Unit.ServiceHandlers
{
    public class BaseHandlerServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly FakePedidoHandlerService _handlerService;

        public BaseHandlerServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _handlerService = new FakePedidoHandlerService(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetById_Should_Call_Mediator_With_SelectOneCommand()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedDto = new PedidoDTO { Id = id };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SelectOneCommand<Pedido, PedidoDTO>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _handlerService.GetById(id);

            // Assert
            Assert.Equal(expectedDto, result);
            _mediatorMock.Verify(m => m.Send(It.Is<SelectOneCommand<Pedido, PedidoDTO>>(c => c.Id == id), default), Times.Once);
        }

        [Fact]
        public async Task GetAll_Should_Call_Mediator_With_SelectAllCommand()
        {
            // Arrange
            var orderBy = "Nome";
            var expected = new List<PedidoDTO>();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SelectAllCommand<Pedido, PedidoDTO>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _handlerService.GetAll(orderBy);

            // Assert
            Assert.Equal(expected, result);
            _mediatorMock.Verify(m => m.Send(It.Is<SelectAllCommand<Pedido, PedidoDTO>>(c => c.OrderBy == orderBy), default), Times.Once);
        }

        [Fact]
        public async Task GetByFilter_Should_Call_Mediator_With_SelectByFilterCommand()
        {
            // Arrange
            List<Dictionary<string, string>> keyValuePairs = new List<Dictionary<string, string>>();

            var filterCommand = new SelectByFilterCommand<Pedido, PedidoDTO>(keyValuePairs, "DataCriacao");
            var expected = new List<PedidoDTO>();
            _mediatorMock
                .Setup(m => m.Send(filterCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _handlerService.GetByFilter(filterCommand);

            // Assert
            Assert.Equal(expected, result);
            _mediatorMock.Verify(m => m.Send(filterCommand, default), Times.Once);
        }

        [Fact]
        public async Task Insert_Should_Call_Mediator_With_InsertCommand()
        {
            // Arrange
            var insertCommand = new InsertCommand<PedidoDTO>(Mock.Of<PedidoDTO>());
            var expectedId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(insertCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _handlerService.Insert(insertCommand);

            // Assert
            Assert.Equal(expectedId, result);
            _mediatorMock.Verify(m => m.Send(insertCommand, default), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Call_Mediator_With_UpdateCommand()
        {
            // Arrange
            var updateCommand = new UpdateCommand<PedidoDTO>(Mock.Of<PedidoDTO>());
            _mediatorMock
                .Setup(m => m.Send(updateCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handlerService.Update(updateCommand);

            // Assert
            Assert.True(result);
            _mediatorMock.Verify(m => m.Send(updateCommand, default), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Mediator_With_DeleteCommand()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteCommand<PedidoDTO>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handlerService.Delete(id);

            // Assert
            Assert.True(result);
            _mediatorMock.Verify(m => m.Send(It.Is<DeleteCommand<PedidoDTO>>(c => c.Id == id), default), Times.Once);
        }

        // Fake para testar a classe abstrata
        private class FakePedidoHandlerService : BaseHandlerService<Pedido, PedidoDTO>
        {
            public FakePedidoHandlerService(IMediator mediator) : base(mediator) { }
        }
    }
}
