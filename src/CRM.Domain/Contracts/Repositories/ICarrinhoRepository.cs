using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface ICarrinhoRepository : IBaseRepository<Carrinho>
{
    void Editar(Carrinho carrinho);
    Task<Carrinho?> ObterPorId(int id);
}