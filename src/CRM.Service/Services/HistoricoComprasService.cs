using AutoMapper;
using CRM.Core.Authorization;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.HistoricoComprasDto;
using CRM.Service.NotificatorConfig;
using FluentValidation.Results;

namespace CRM.Service.Services;

public class HistoricoComprasService : BaseService, IHistoricoComprasService
{
    public HistoricoComprasService(IMapper mapper, INotificator notificator, IHistoricoComprasRepository historicoComprasRepository, IAuthenticatedUser authenticatedUser, ICarrinhoRepository carrinhoRepository) : base(mapper, notificator)
    {
        _historicoComprasRepository = historicoComprasRepository;
        _authenticatedUser = authenticatedUser;
        _carrinhoRepository = carrinhoRepository;
    }

    private readonly IHistoricoComprasRepository _historicoComprasRepository; 
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IAuthenticatedUser _authenticatedUser;

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

        foreach (var produtoCarrinho in carrinho.ProdutoCarrinhos)
        {
            var compra = new Compra
            {
                UserId = _authenticatedUser.Id,
                HistoricoId = historico.Id,
                ProdutoId = produtoCarrinho.ProdutoId,
                Quantidade = produtoCarrinho.Quantidade,
                ValorTotal = produtoCarrinho.ValorTotal
            };
            
            historico.Compras.Add(compra);
            historico.ValorTotal += compra.ValorTotal;
        }
        
        if(!Validar(historico)) return;
        
        _historicoComprasRepository.Comprando(historico);
        if(!await Commit()) Notificator.Handle("Não foi possível salvar no histórico de compras.");
    }

    public async Task<HistoricoComprasDto?> ObterPorId(int id)
    {
        var historico = await _historicoComprasRepository.ObterPorId(id);
        if (historico != null) return Mapper.Map<HistoricoComprasDto>(historico);
        
        Notificator.HandleNotFound();
        return null;
    }

    private async Task<bool> Commit() => await _historicoComprasRepository.UnitOfWork.Commit();

    private bool Validar(HistoricoCompras historicoCompras)
    {
        if (historicoCompras.Validar(out ValidationResult validationResult)) return true;
        
        Notificator.Handle(validationResult.Errors);
        return false;
    }
}