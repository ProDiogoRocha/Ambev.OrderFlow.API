using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.Commands;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.OrderFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevendaController : ControllerBase
    {
        private readonly IEmissorHandlerService _revendaAppService;
        public RevendaController(IEmissorHandlerService revendaAppService)
        {
            _revendaAppService = revendaAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _revendaAppService.GetById(id);
            
            return response != null ? Ok(response) : NotFound();
        }

        [HttpGet("all/{orderBy}")]
        public async Task<IActionResult> GetAllOrderBy(string orderBy)
        {
            var response = await _revendaAppService.GetAll(orderBy);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost("filter/")]
        public async Task<IActionResult> GetAllOrderBy(SelectByFilterCommand<Revenda, RevendaDTO> selectByFilterCommand)
        {
            var response = await _revendaAppService.GetByFilter(selectByFilterCommand);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertCommand<RevendaDTO> insertRevendaCommand)
        {
            var response = await _revendaAppService.Insert(insertRevendaCommand);
            return Guid.Empty != response ? Ok(response) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommand<RevendaDTO> updateRevendaCommand)
        {
            var response = await _revendaAppService.Update(updateRevendaCommand);
            return response ? Ok(response) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var response = await _revendaAppService.Delete(id);
            return response ? Ok(response) : BadRequest();
        }

        [HttpGet("emitirpedido/{idRevenda}")]
        public async Task<IActionResult> EmitirPedido(Guid idRevenda)
        {
            var response = await _revendaAppService.EmitirPedidosRevenda(idRevenda);
            return response.Count() > 0 ? Ok(response) : BadRequest();
        }
    }
}
