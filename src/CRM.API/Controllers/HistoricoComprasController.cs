using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.HistoricoComprasDto;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers;

[Microsoft.AspNetCore.Components.Route("v1/HistoricoCompras")]
public class HistoricoComprasController :BaseController
{
    public HistoricoComprasController(INotificator notificator, IHistoricoComprasService historicoComprasService) : base(notificator)
    {
        _historicoComprasService = historicoComprasService;
    }

    private readonly IHistoricoComprasService _historicoComprasService;
    
    [HttpPost("Comprando")]
    [Authorize]
    [SwaggerOperation(Summary = "Realiza uma compra.", Tags = new[] { "HistoricoCompras" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Comprando([FromBody] ComprandoDto dto)
    {
        await _historicoComprasService.Comprando(dto);
        return CreatedResponse(string.Empty);
    }
    
    [HttpGet("ObterPorId/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Obtêm um histórico de compras por id.", Tags = new[] { "HistoricoCompras" })]
    [ProducesResponseType(typeof(HistoricoComprasDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var historico = await _historicoComprasService.ObterPorId(id);
        return OkResponse(historico);
    }
}