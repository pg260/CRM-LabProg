﻿using AutoMapper;
using CRM.Core.Authorization;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.HistoricoComprasDto;
using CRM.Service.Dtos.PaginatedSearch;
using CRM.Service.NotificatorConfig;
using FluentValidation.Results;

namespace CRM.Service.Services;

public class HistoricoComprasService : BaseService, IHistoricoComprasService
{
    public HistoricoComprasService(IMapper mapper, INotificator notificator,
        IHistoricoComprasRepository historicoComprasRepository,
        IAuthenticatedUser authenticatedUser, ICarrinhoRepository carrinhoRepository, ICarrinhoService carrinhoService,
        IProdutoRepository produtoRepository) : base(mapper, notificator)
    {
        _historicoComprasRepository = historicoComprasRepository;
        _authenticatedUser = authenticatedUser;
        _carrinhoRepository = carrinhoRepository;
        _carrinhoService = carrinhoService;
        _produtoRepository = produtoRepository;
    }

    private readonly IHistoricoComprasRepository _historicoComprasRepository;
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly ICarrinhoService _carrinhoService;
    private readonly IProdutoRepository _produtoRepository;

    public async Task Comprando(ComprandoDto dto)
    {
        var carrinho = await _carrinhoRepository.ObterPorId(dto.CarrinhoId);
        if (carrinho == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        if (carrinho.ProdutoCarrinhos.Count <= 0)
        {
            Notificator.Handle("O seu carriho está vazio.");
            return;
        }

        var historico = new HistoricoCompras
        {
            UserId = _authenticatedUser.Id,
            ValorTotal = 0,
            MetodoDePagameto = dto.MetodoPagamento
        };

        var produtosAtualizados = new List<Produto>();

        foreach (var produtoCarrinho in carrinho.ProdutoCarrinhos)
        {
            var produto = await _produtoRepository.FirstOrDefault(c => c.Id == produtoCarrinho.ProdutoId);
            if (produto == null || produto.Desativado)
            {
                Notificator.Handle(
                    "Alguns dos produtos estão desativados ou não existem, atualize a página e tente novamente.");
                return;
            }

            var compra = new Compra
            {
                UserId = _authenticatedUser.Id,
                HistoricoComprasId = historico.Id,
                ProdutoId = produtoCarrinho.ProdutoId,
                Quantidade = produtoCarrinho.Quantidade,
                ValorTotal = produtoCarrinho.ValorTotal
            };

            historico.Compras.Add(compra);
            historico.ValorTotal += compra.ValorTotal;

            produto.TotalVendas++;
            produtosAtualizados.Add(produto);
        }

        carrinho.ProdutoCarrinhos.Clear();

        if (!Validar(historico)) return;

        _historicoComprasRepository.Comprando(historico);

        foreach (var produto in produtosAtualizados)
        {
            _produtoRepository.Editar(produto);
        }

        if (!await Commit()) Notificator.Handle("Não foi possível salvar no histórico de compras.");

        await _carrinhoService.EsvaziandoCarrinho(carrinho.Id);
    }

    public async Task<HistoricoComprasDto?> ObterPorId(int id)
    {
        var historico = await _historicoComprasRepository.ObterPorId(id);
        if (historico != null) return Mapper.Map<HistoricoComprasDto>(historico);

        Notificator.HandleNotFound();
        return null;
    }

    public async Task<PagedDto<HistoricoComprasDto>> Buscar(BuscarHistoricoComprasDto dto)
    {
        var busca = await _historicoComprasRepository.Search(dto);
        return Mapper.Map<PagedDto<HistoricoComprasDto>>(busca);
    }

    private async Task<bool> Commit() => await _historicoComprasRepository.UnitOfWork.Commit();

    private bool Validar(HistoricoCompras historicoCompras)
    {
        if (historicoCompras.Validar(out ValidationResult validationResult)) return true;

        Notificator.Handle(validationResult.Errors);
        return false;
    }
}