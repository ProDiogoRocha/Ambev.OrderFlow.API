using Ambev.OrderFlow.API.Controllers;
using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Ambev.OrderFlow.Test.Unit.Controllers
{
    public class RevendaControllerTests
    {
        private readonly Mock<IEmissorHandlerService> _mockRevendaAppService;
        private readonly RevendaController _controller;

        public RevendaControllerTests()
        {
            _mockRevendaAppService = new Mock<IEmissorHandlerService>();
            _controller = new RevendaController(_mockRevendaAppService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenRevendaIsFound()
        {
            // Arrange
            var revendaId = Guid.NewGuid();
            var revendaDTO = new RevendaDTO { /* Preencha com dados válidos */ };
            _mockRevendaAppService.Setup(service => service.GetById(revendaId))
                                  .ReturnsAsync(revendaDTO);

            // Act
            var result = await _controller.GetById(revendaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(revendaDTO, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenRevendaIsNotFound()
        {
            // Arrange
            var revendaId = Guid.NewGuid();
            _mockRevendaAppService.Setup(service => service.GetById(revendaId))
                                  .ReturnsAsync((RevendaDTO)null);

            // Act
            var result = await _controller.GetById(revendaId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllOrderBy_ReturnsOkResult_WhenRevendasAreFound()
        {
            // Arrange
            var orderBy = "name";
            var revendasDTO = new List<RevendaDTO> { /* Preencha com dados válidos */ };
            _mockRevendaAppService.Setup(service => service.GetAll(orderBy))
                                  .ReturnsAsync(revendasDTO);

            // Act
            var result = await _controller.GetAllOrderBy(orderBy);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(revendasDTO, okResult.Value);
        }

        [Fact]
        public async Task GetAllOrderBy_ReturnsNotFound_WhenRevendasAreNotFound()
        {
            // Arrange
            var orderBy = "name";
            _mockRevendaAppService.Setup(service => service.GetAll(orderBy))
                                  .ReturnsAsync((List<RevendaDTO>)null);

            // Act
            var result = await _controller.GetAllOrderBy(orderBy);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenRevendaIsCreated()
        {
            // Arrange
            var insertCommand = new InsertCommand<RevendaDTO>(Mock.Of<RevendaDTO>());
            var revendaId = Guid.NewGuid();
            _mockRevendaAppService.Setup(service => service.Insert(insertCommand))
                                  .ReturnsAsync(revendaId);

            // Act
            var result = await _controller.Create(insertCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(revendaId, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenRevendaIsNotCreated()
        {
            // Arrange
            var insertCommand = new InsertCommand<RevendaDTO>(Mock.Of<RevendaDTO>());
            _mockRevendaAppService.Setup(service => service.Insert(insertCommand))
                                  .ReturnsAsync(Guid.Empty);

            // Act
            var result = await _controller.Create(insertCommand);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenRevendaIsUpdated()
        {
            // Arrange
            var updateCommand = new UpdateCommand<RevendaDTO>(Mock.Of<RevendaDTO>());
            _mockRevendaAppService.Setup(service => service.Update(updateCommand))
                                  .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(updateCommand);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenRevendaIsNotUpdated()
        {
            // Arrange
            var updateCommand = new UpdateCommand<RevendaDTO>(Mock.Of<RevendaDTO>());
            _mockRevendaAppService.Setup(service => service.Update(updateCommand))
                                  .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(updateCommand);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsOkResult_WhenRevendaIsRemoved()
        {
            // Arrange
            var revendaId = Guid.NewGuid();
            _mockRevendaAppService.Setup(service => service.Delete(revendaId))
                                  .ReturnsAsync(true);

            // Act
            var result = await _controller.Remove(revendaId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsBadRequest_WhenRevendaIsNotRemoved()
        {
            // Arrange
            var revendaId = Guid.NewGuid();
            _mockRevendaAppService.Setup(service => service.Delete(revendaId))
                                  .ReturnsAsync(false);

            // Act
            var result = await _controller.Remove(revendaId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task EmitirPedido_ReturnsOkResult_WhenPedidosAreEmitted()
        {
            // Arrange
            var revendaId = Guid.NewGuid();
            var pedidos = new List<Guid> 
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };
            _mockRevendaAppService.Setup(service => service.EmitirPedidosRevenda(revendaId))
                                  .ReturnsAsync(pedidos);

            // Act
            var result = await _controller.EmitirPedido(revendaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pedidos, okResult.Value);
        }

        [Fact]
        public async Task EmitirPedido_ReturnsBadRequest_WhenNoPedidosAreEmitted()
        {
            // Arrange
            var revendaId = Guid.NewGuid();
            _mockRevendaAppService.Setup(service => service.EmitirPedidosRevenda(revendaId))
                                  .ReturnsAsync(new List<Guid>());

            // Act
            var result = await _controller.EmitirPedido(revendaId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
