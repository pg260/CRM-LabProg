using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IComprasRepository : IBaseRepository<Compra>
{
    Task<int> CalculandoTotalVendas(int id);
}