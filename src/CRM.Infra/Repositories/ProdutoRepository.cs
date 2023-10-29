using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(BaseDbContext context) : base(context)
    { }

    public async Task<Produto?> ObterPorId(int id)
    {
        return await Context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}