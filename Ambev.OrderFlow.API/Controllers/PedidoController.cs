using Ambev.OrderFlow.Application.AppServices.Implementations;
using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.OrderFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IHandlerService<Pedido, PedidoDTO> _PedidoAppService;
        public PedidoController(IHandlerService<Pedido, PedidoDTO> pedidoAppService)
        {
            _PedidoAppService = pedidoAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _PedidoAppService.GetById(id);

            return response != null ? Ok(response) : NotFound();
        }

        [HttpGet("all/{orderBy}")]
        public async Task<IActionResult> GetAllOrderBy(string orderBy)
        {
            var response = await _PedidoAppService.GetAll(orderBy);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost("filter/")]
        public async Task<IActionResult> GetAllOrderBy(SelectByFilterCommand<Pedido, PedidoDTO> selectByFilterCommand)
        {
            var response = await _PedidoAppService.GetByFilter(selectByFilterCommand);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertCommand<PedidoDTO> insertPedidoCommand)
        {
            var response = await _PedidoAppService.Insert(insertPedidoCommand);
            return Guid.Empty != response ? Ok(response) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommand<PedidoDTO> updatePedidoCommand)
        {
            var response = await _PedidoAppService.Update(updatePedidoCommand);
            return response ? Ok(response) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var response = await _PedidoAppService.Delete(id);
            return response ? Ok(response) : BadRequest();
        }
    }
}
