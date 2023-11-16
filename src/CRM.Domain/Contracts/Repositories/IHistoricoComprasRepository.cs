using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IHistoricoComprasRepository : IBaseRepository<HistoricoCompras>
{
    void Comprando(HistoricoCompras historicoCompras);
    Task<HistoricoCompras?> ObterPorId(int id);
}