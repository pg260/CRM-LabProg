using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.Dtos.ProdutoDtos;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers
{
    [Route("v1/Produto")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(INotificator notificator, IProdutoService produtoService) : base(notificator)
        {
            _produtoService = produtoService;
        }

        [HttpPost("criar")]
        [SwaggerOperation(Summary = "Cadastrar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Criar([FromBody] AddProdutoDto dto)
        {
            await _produtoService.Criar(dto);
            return CreatedResponse(string.Empty);
        }

        [HttpPut("editar/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Editar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Editar(int id, [FromBody] ProdutoDto dto)
        {
            await _produtoService.Editar(id, dto);
            return NoContentResponse();
        }

        [HttpPatch("ativar/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Ativar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Ativar(int id)
        {
            await _produtoService.Ativar(id);
            return OkResponse();
        }
        
        [HttpPatch("desativar/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Desativar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Desativar(int id)
        {
            await _produtoService.Desativar(id);
            return OkResponse();
        }

        [HttpGet("obter/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Obter um Produto por id.", Tags = new[] { "Produto" })]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var produto = await _produtoService.ObterPorId(id);
            return OkResponse(produto);
        }

        [HttpGet("obter")]
        [Authorize]
        [SwaggerOperation(Summary = "Pesquisar um ou mais Produtos.", Tags = new[] { "Produto" })]
        [ProducesResponseType(typeof(PagedDto<ProdutoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Pesquisar([FromQuery] BuscarProdutoDto dto)
        {
            var produtos = await _produtoService.Pesquisar(dto);
            return OkResponse(produtos);
        }
    }
}