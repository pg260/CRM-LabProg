using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.UserDtos;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers;

[Route("v1/User")]
public class UserController : BaseController
{
    public UserController(INotificator notificator, IUserService userService) : base(notificator)
    {
        _userService = userService;
    }

    private readonly IUserService _userService;
    
    [HttpPost("Criar")]
    [SwaggerOperation(Summary = "Cria um usuário.", Tags = new[] { "User" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Criar([FromBody] CreateUserDto dto)
    {
        await _userService.Criar(dto);
        return CreatedResponse(string.Empty);
    }

    [HttpPut("Editar/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Edita um usuário.", Tags = new[] { "User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar(int id, [FromBody] UpdateUserDto dto)
    {
        await _userService.Editar(id, dto);
        return NoContentResponse();
    }
    
    [HttpDelete("Remover/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Remove um usuário.", Tags = new[] { "User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar(int id)
    {
        await _userService.Remover(id);
        return NoContentResponse();
    }
    
    [HttpGet("ObterPorId/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Obtêm um usuário por id.", Tags = new[] { "User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var user = await _userService.ObterPorId(id);
        return OkResponse(user);
    }
    
    [HttpGet("Pesquisar")]
    [Authorize]
    [SwaggerOperation(Summary = "Pesquisa um ou mais usuários.", Tags = new[] { "User" })]
    [ProducesResponseType(typeof(PagedDto<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Pesquisar([FromQuery] SearchUserDto dto)
    {
        var users = await _userService.Pesquisar(dto);
        return OkResponse(users);
    }
}