using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.AuthDtos;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers;

[Route("v1/Auth")]
public class AuthController : BaseController
{
    
    public AuthController(INotificator notificator, IAuthService authService) : base(notificator)
    {
        _authService = authService;
    }

    private readonly IAuthService _authService;
    
    [HttpPost]
    [SwaggerOperation(Summary = "Logar", Tags = new[] { "Auth" })]
    [ProducesResponseType(typeof(AuthUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.Login(dto);
        return CreatedResponse(string.Empty, token);
    }
}