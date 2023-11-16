using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class HistoricoComprasRepository : BaseRepository<HistoricoCompras>, IHistoricoComprasRepository
{
    public HistoricoComprasRepository(BaseDbContext context) : base(context)
    { }

    public void Comprando(HistoricoCompras historicoCompras) => Context.HistoricoCompras.Add(historicoCompras);

    public async Task<HistoricoCompras?> ObterPorId(int id)
    {
        return await Context.HistoricoCompras
            .AsNoTracking()
            .Include(c => c.Compras)
            .ThenInclude(c => c.Produto)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}