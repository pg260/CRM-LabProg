using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.ProdutoDtos;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers
{
    [Microsoft.AspNetCore.Components.Route("v1/Produto")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(INotificator notificator, IProdutoService produtoService) : base(notificator)
        {
            _produtoService = produtoService;
        }

        [HttpPost("criar")]
        [SwaggerOperation(Summary = "Cadastrar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Criar([FromBody] AddProdutoDto dto)
        {
            var produto = await _produtoService.Criar(dto);
            return CreatedResponse(string.Empty, produto);
        }

        [HttpPut("editar/{id}")]
        [SwaggerOperation(Summary = "Editar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Editar(int id, [FromBody] ProdutoDto dto)
        {
            var produto = await _produtoService.Editar(id, dto);
            return OkResponse(produto);
        }

        [HttpPatch("ativar/{id}")]
        [SwaggerOperation(Summary = "Ativar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [SwaggerOperation(Summary = "Desativar um Produto.", Tags = new[] { "Produto" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [SwaggerOperation(Summary = "Pesquisar um ou mais Produtos.", Tags = new[] { "Produto" })]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
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