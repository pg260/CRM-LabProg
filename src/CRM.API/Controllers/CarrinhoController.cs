﻿using CRM.API.Responses;
using CRM.Service.Contracts;
using CRM.Service.Dtos.CarrinhoDtos;
using CRM.Service.Dtos.ProdutoCarrinhoDtos;
using CRM.Service.NotificatorConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.API.Controllers;

[Route("v1/Carrinho")]
public class CarrinhoController : BaseController
{
    public CarrinhoController(INotificator notificator, ICarrinhoService carrinhoService) : base(notificator)
    {
        _carrinhoService = carrinhoService;
    }

    private readonly ICarrinhoService _carrinhoService;
    
    [HttpPut("AdicionarProduto/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Adiciona um produto.", Tags = new[] { "Carrinho" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AdicionarProduto(int id, [FromBody] AlterarProdutoCarrinhoDto dto)
    {
        await _carrinhoService.AdicionarProduto(id, dto);
        return NoContentResponse();
    }
    
    [HttpDelete("RemoverProduto/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Remove um produto.", Tags = new[] { "Carrinho" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverProduto(int id, [FromBody] AlterarProdutoCarrinhoDto dto)
    {
        await _carrinhoService.RemoverProduto(id, dto);
        return NoContentResponse();
    }
    
    [HttpDelete("EsvaziandoCarrinho/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Esvazia o carrinho.", Tags = new[] { "Carrinho" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EsvaziandoCarrinho(int id)
    {
        await _carrinhoService.EsvaziandoCarrinho(id);
        return NoContentResponse();
    }
    
    [HttpGet("ObterPorId/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Obtem um carrinho por id.", Tags = new[] { "Carrinho" })]
    [ProducesResponseType(typeof(CarrinhoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var carrinho = await _carrinhoService.ObterPorId(id);
        return OkResponse(carrinho);
    }
}