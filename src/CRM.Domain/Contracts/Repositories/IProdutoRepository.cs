using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    Task<Produto?> ObterPorId(int id);
}