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
    public CarrinhoService(IMapper mapper, INotificator notificator, ICarrinhoRepository carrinhoRepository, IAuthenticatedUser authenticatedUser, IProdutoRepository produtoRepository) : base(mapper, notificator)
    {
        _carrinhoRepository = carrinhoRepository;
        _authenticatedUser = authenticatedUser;
        _produtoRepository = produtoRepository;
    }

    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IProdutoRepository _produtoRepository;

    public async Task<CarrinhoDto?> ObterPorId(int id)
    {
        var carrinho = await _carrinhoRepository.ObterPorId(id);
        if (carrinho != null) return Mapper.Map<CarrinhoDto>(carrinho);
        
        Notificator.HandleNotFound();
        return null;
    }

    public async Task AdicionarProduto(int carrinhoId, AlterarProdutoCarrinhoDto dto)
    {
        if(!await Validar(carrinhoId, dto)) return;

        var carrinho = await _carrinhoRepository.FirstOrDefault(c => c.Id == carrinhoId);
        var contagemProdutos = 0;
        foreach (var produtos in carrinho!.ProdutoCarrinhos.Where(produtos => produtos.ProdutoId == dto.ProdutoId))
        {
            carrinho.ProdutoCarrinhos.Remove(produtos);
            contagemProdutos++;
        }
        
        var produtoCarrinho = Mapper.Map<ProdutoCarrinho>(dto);
        carrinho.ProdutoCarrinhos.Add(produtoCarrinho);
        carrinho.ValorTotal += (dto.Quantidade + contagemProdutos) * dto.ValorProduto;

        if (!await Commit()) Notificator.Handle("Não foi possível adicionar esse item ao carrinho.");
    }

    public async Task RemoverProduto(int carrinhoId,  AlterarProdutoCarrinhoDto dto)
    {
        if(!await Validar(carrinhoId, dto)) return;
        
        var carrinho = await _carrinhoRepository.FirstOrDefault(c => c.Id == carrinhoId);
        var contagemProdutos = dto.Quantidade;
        foreach (var produtos in carrinho!.ProdutoCarrinhos.Where(produtos => produtos.ProdutoId == dto.ProdutoId))
        {
            carrinho.ProdutoCarrinhos.Remove(produtos);
            contagemProdutos--;
            if(contagemProdutos == 0) break;
        }

        carrinho.ValorTotal -= dto.Quantidade * dto.ValorProduto;
        if (!await Commit()) Notificator.Handle("Não foi possível adicionar esse item ao carrinho.");
    }

    private async Task<bool> Commit() => await _carrinhoRepository.UnitOfWork.Commit();

    private async Task<bool> Validar(int carrinhoId, AlterarProdutoCarrinhoDto dto)
    {
        if (carrinhoId != dto.CarrinhoId)
        {
            Notificator.Handle("Os ids do carrinho não conferem.");
            return false;
        }

        if (_authenticatedUser.Id == carrinhoId) return true;
        
        var produto = await _produtoRepository.FirstOrDefault(c => c.Id == dto.ProdutoId);
        if (produto == null)
        {
            Notificator.Handle("Não existe um produto com esse id.");
            return false;
        }

        if (Math.Abs(produto.Valor - dto.ValorProduto) > 0.00001)
        {
            Notificator.Handle("Os valor do produto está incorreto, atualize a pagina e tente novamente.");
            return false;
        }
        
        Notificator.Handle("Você não pode adicionar produtos no carrinho de outra pessoa.");
        return false;
    }
}