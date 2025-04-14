using Ambev.OrderFlow.API.Controllers;
using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Ambev.OrderFlow.Test.Unit.Controllers
{
    public class PedidoControllerTests
    {
        private readonly Mock<IHandlerService<Pedido, PedidoDTO>> _mockPedidoAppService;
        private readonly PedidoController _controller;

        public PedidoControllerTests()
        {
            _mockPedidoAppService = new Mock<IHandlerService<Pedido, PedidoDTO>>();
            _controller = new PedidoController(_mockPedidoAppService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenPedidoIsFound()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();
            var pedidoDTO = Mock.Of<PedidoDTO>();
            _mockPedidoAppService.Setup(service => service.GetById(pedidoId))
                                 .ReturnsAsync(pedidoDTO);

            // Act
            var result = await _controller.GetById(pedidoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pedidoDTO, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenPedidoIsNotFound()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();
            _mockPedidoAppService.Setup(service => service.GetById(pedidoId))
                                 .ReturnsAsync((PedidoDTO)null);

            // Act
            var result = await _controller.GetById(pedidoId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllOrderBy_ReturnsOkResult_WhenPedidosAreFound()
        {
            // Arrange
            var orderBy = "name";
            var pedidosDTO = new List<PedidoDTO>()
            {
                Mock.Of<PedidoDTO>(),
                Mock.Of<PedidoDTO>()
            };
            _mockPedidoAppService.Setup(service => service.GetAll(orderBy))
                                 .ReturnsAsync(pedidosDTO);

            // Act
            var result = await _controller.GetAllOrderBy(orderBy);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pedidosDTO, okResult.Value);
        }

        [Fact]
        public async Task GetAllOrderBy_ReturnsNotFound_WhenPedidosAreNotFound()
        {
            // Arrange
            var orderBy = "name";
            _mockPedidoAppService.Setup(service => service.GetAll(orderBy))
                                 .ReturnsAsync((List<PedidoDTO>)null);

            // Act
            var result = await _controller.GetAllOrderBy(orderBy);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenPedidoIsCreated()
        {
            // Arrange
            var insertCommand = new InsertCommand<PedidoDTO>(Mock.Of<PedidoDTO>());
            var pedidoId = Guid.NewGuid();
            _mockPedidoAppService.Setup(service => service.Insert(insertCommand))
                                 .ReturnsAsync(pedidoId);

            // Act
            var result = await _controller.Create(insertCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pedidoId, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenPedidoIsNotCreated()
        {
            // Arrange
            var insertCommand = new InsertCommand<PedidoDTO>(Mock.Of<PedidoDTO>());
            _mockPedidoAppService.Setup(service => service.Insert(insertCommand))
                                 .ReturnsAsync(Guid.Empty);

            // Act
            var result = await _controller.Create(insertCommand);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenPedidoIsUpdated()
        {
            // Arrange
            var updateCommand = new UpdateCommand<PedidoDTO>(Mock.Of<PedidoDTO>());
            _mockPedidoAppService.Setup(service => service.Update(updateCommand))
                                 .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(updateCommand);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenPedidoIsNotUpdated()
        {
            // Arrange
            var updateCommand = new UpdateCommand<PedidoDTO>(Mock.Of<PedidoDTO>());
            _mockPedidoAppService.Setup(service => service.Update(updateCommand))
                                 .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(updateCommand);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsOkResult_WhenPedidoIsRemoved()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();
            _mockPedidoAppService.Setup(service => service.Delete(pedidoId))
                                 .ReturnsAsync(true);

            // Act
            var result = await _controller.Remove(pedidoId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsBadRequest_WhenPedidoIsNotRemoved()
        {
            // Arrange
            var pedidoId = Guid.NewGuid();
            _mockPedidoAppService.Setup(service => service.Delete(pedidoId))
                                 .ReturnsAsync(false);

            // Act
            var result = await _controller.Remove(pedidoId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
