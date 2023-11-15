using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;

namespace CRM.Infra.Repositories;

public class ProdutoCarrinhoRepository : BaseRepository<ProdutoCarrinho>, IProdutoCarrinhoRepository
{
    public ProdutoCarrinhoRepository(BaseDbContext context) : base(context)
    { }

    public void Editar(ProdutoCarrinho produtoCarrinho) => Context.ProdutoCarrinhos.Update(produtoCarrinho);
    public void Remover(ProdutoCarrinho produtoCarrinho) => Context.ProdutoCarrinhos.Remove(produtoCarrinho);
}