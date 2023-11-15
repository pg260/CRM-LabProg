using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IProdutoCarrinhoRepository : IBaseRepository<ProdutoCarrinho>
{
    void Editar(ProdutoCarrinho produtoCarrinho);
    void Remover(ProdutoCarrinho produtoCarrinho);
}