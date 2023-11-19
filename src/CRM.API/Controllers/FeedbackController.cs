using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.Feedback;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers;

[Route("v1/Feedback")]
public class FeedbackController : BaseController
{
    public FeedbackController(INotificator notificator, IFeedbackService feedbackService) : base(notificator)
    {
        _feedbackService = feedbackService;
    }

    private readonly IFeedbackService _feedbackService;
    
    [HttpPost("avaliando")]
    [Authorize]
    [SwaggerOperation(Summary = "Realiza uma avaliação.", Tags = new[] { "Feedback" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Comprando([FromBody] AddFeedbackDto dto)
    {
        await _feedbackService.Criar(dto);
        return CreatedResponse(string.Empty);
    }
    
    [HttpGet("obterPorId/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Obtêm uma avaliacao de produto por id.", Tags = new[] { "Feedback" })]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var historico = await _feedbackService.ObterPorId(id);
        return OkResponse(historico);
    }
    
    [HttpGet("buscar")]
    [Authorize]
    [SwaggerOperation(Summary = "Realiza uma busca de feedbacks.", Tags = new[] { "Feedback" })]
    [ProducesResponseType(typeof(PagedDto<FeedbackDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Buscar([FromQuery] BuscarFeedbackDto dto)
    {
        var historico = await _feedbackService.Buscar(dto);
        return OkResponse(historico);
    }
    
    [HttpDelete("remover/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Remove um feedback.", Tags = new[] { "Feedback" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        await _feedbackService.Excluir(id);
        return NoContentResponse();
    }
}