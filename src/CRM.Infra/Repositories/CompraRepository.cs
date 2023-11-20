using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class CompraRepository : BaseRepository<Compra>, IComprasRepository
{
    public CompraRepository(BaseDbContext context) : base(context)
    { }
    
    public async Task<int> CalculandoTotalVendas(int id)
    {
        return await Context.Compras
            .AsNoTracking()
            .CountAsync(c => c.ProdutoId == id);
    }
}