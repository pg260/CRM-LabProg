using AutoMapper;
using CRM.Core.Authorization;
using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Service.Contracts;
using CRM.Service.Dtos.CarrinhoDtos;
using CRM.Service.Dtos.ProdutoCarrinhoDtos;
using CRM.Service.NotificatorConfig;

namespace CRM.Service.Services;

public class CarrinhoService : BaseService, ICarrinhoService
{
    public CarrinhoService(IMapper mapper, INotificator notificator, ICarrinhoRepository carrinhoRepository,
        IAuthenticatedUser authenticatedUser, IProdutoRepository produtoRepository, IProdutoCarrinhoRepository produtoCarrinhoRepository) : base(mapper, notificator)
    {
        _carrinhoRepository = carrinhoRepository;
        _authenticatedUser = authenticatedUser;
        _produtoRepository = produtoRepository;
        _produtoCarrinhoRepository = produtoCarrinhoRepository;
    }

    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoCarrinhoRepository _produtoCarrinhoRepository;

    public async Task<CarrinhoDto?> ObterPorId(int id)
    {
        if (!await AtualizandoCarrinho(id)) return null;

        var carrinho = await _carrinhoRepository.ObterPorId(id);
        if (carrinho != null) return Mapper.Map<CarrinhoDto>(carrinho);

        Notificator.HandleNotFound();
        return null;
    }

    public async Task AdicionarProduto(int carrinhoId, AlterarProdutoCarrinhoDto dto)
    {
        if (!Validar(carrinhoId, dto)) return;

        var carrinho = await _carrinhoRepository.ObterPorId(carrinhoId);
        if (carrinho == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        var produto = await _produtoRepository.FirstOrDefault(c => c.Id == dto.ProdutoId && !c.Desativado);
        if (produto == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        var contagemProdutos = dto.Quantidade;
        
        var produtoExistente = carrinho.ProdutoCarrinhos.Find(c => c.ProdutoId == dto.ProdutoId);
        if (produtoExistente != null)
        {
            produtoExistente.Quantidade += dto.Quantidade;
            produtoExistente.ValorTotal += dto.Quantidade * produto.Valor;
            
            _produtoCarrinhoRepository.Editar(produtoExistente);
            if (!await CommitProdutoCarrinho()) Notificator.Handle("Não foi possível adicionar esse item ao carrinho.");
            return;
        }

        var produtoCarrinho = Mapper.Map<ProdutoCarrinho>(dto);
        produtoCarrinho.ValorTotal = dto.Quantidade * produto.Valor;
        
        for (var i = 0; i < contagemProdutos; i++) carrinho.ProdutoCarrinhos.Add(produtoCarrinho);
        carrinho.ValorTotal += produtoCarrinho.ValorTotal;

        _carrinhoRepository.Editar(carrinho);
        if (!await Commit()) Notificator.Handle("Não foi possível adicionar esse item ao carrinho.");
    }

    public async Task RemoverProduto(int carrinhoId, AlterarProdutoCarrinhoDto dto)
    {
        if (!Validar(carrinhoId, dto)) return;

        var carrinho = await _carrinhoRepository.ObterPorId(carrinhoId);
        if (carrinho == null)
        {
            Notificator.HandleNotFound();
            return;
        }
        
        var produto = await _produtoRepository.FirstOrDefault(c => c.Id == dto.ProdutoId && !c.Desativado);
        if (produto == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        var produtoExistente = carrinho.ProdutoCarrinhos.Find(c => c.ProdutoId == dto.ProdutoId);
        if (produtoExistente != null)
        {
            produtoExistente.Quantidade -= dto.Quantidade;
            if (produtoExistente.Quantidade <= 0)
            {
                _produtoCarrinhoRepository.Remover(produtoExistente);
            }
            else
            {
                produtoExistente.ValorTotal -= dto.Quantidade * produto.Valor;
                _produtoCarrinhoRepository.Editar(produtoExistente);
            }
            
            if (!await CommitProdutoCarrinho()) Notificator.Handle("Não foi possível remover esse item ao carrinho.");
            return;
        }
        
        Notificator.Handle("Esse produto não está mais o seu carrinho.");
    }

    public async Task EsvaziandoCarrinho(int carrinhoId)
    {
        var carrinho = await _carrinhoRepository.ObterPorId(carrinhoId);
        if (carrinho == null)
        {
            Notificator.HandleNotFound();
            return;
        }

        foreach (var produtoCarrinho in carrinho.ProdutoCarrinhos)
        {
            _produtoCarrinhoRepository.Remover(produtoCarrinho);   
        }
        
        if(!await CommitProdutoCarrinho()) Notificator.Handle("Não foi possível esvaziar o seu carrinho.");
    }

    private async Task<bool> Commit() => await _carrinhoRepository.UnitOfWork.Commit();
    private async Task<bool> CommitProdutoCarrinho() => await _produtoCarrinhoRepository.UnitOfWork.Commit();
    private bool Validar(int carrinhoId, AlterarProdutoCarrinhoDto dto)
    {
        if (carrinhoId != dto.CarrinhoId)
        {
            Notificator.Handle("Os ids do carrinho não conferem.");
            return false;
        }

        if (_authenticatedUser.Id == carrinhoId) return true;
        
        Notificator.Handle("Você não pode adicionar produtos no carrinho de outra pessoa.");
        return false;
    }

    private async Task<bool> AtualizandoCarrinho(int id)
    {
        var carrinho = await _carrinhoRepository.ObterPorId(id);
        if (carrinho == null)
        {
            Notificator.HandleNotFound();
            return false;
        }

        if (carrinho.ProdutoCarrinhos.Count == 0)
        {
            if (carrinho.ValorTotal != 0)
            {
                carrinho.ValorTotal = 0;
                _carrinhoRepository.Editar(carrinho);

                if (!await Commit())
                {
                    Notificator.Handle("Não foi possível atualizar o carrinho.");
                    return false;
                }
            }
            
            return true;
        }

        foreach (var produto in carrinho.ProdutoCarrinhos.Where(produto => produto.Produto.Desativado))
        {
            carrinho.ValorTotal -= produto.Produto.Valor;
            carrinho.ProdutoCarrinhos.Remove(produto);
        }

        var total = carrinho.ProdutoCarrinhos.Sum(produto => produto.ValorTotal);
        if (Math.Abs(total - carrinho.ValorTotal) > 0) carrinho.ValorTotal = total;

        _carrinhoRepository.Editar(carrinho);
        if (!await Commit()) Notificator.Handle("Não foi possível abrir o carrinho.");
        return true;
    }
}