using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;

namespace CRM.Infra.Repositories;

public class CompraRepository : BaseRepository<Compra>, IComprasRepository
{
    public CompraRepository(BaseDbContext context) : base(context)
    { }
}