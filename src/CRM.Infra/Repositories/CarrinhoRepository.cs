using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class CarrinhoRepository : BaseRepository<Carrinho>, ICarrinhoRepository
{
    public CarrinhoRepository(BaseDbContext context) : base(context)
    { }

    public void Editar(Carrinho carrinho) => Context.Carrinhos.Update(carrinho);

    public async Task<Carrinho?> ObterPorId(int id)
    {
        return await Context.Carrinhos
            .AsNoTracking()
            .Include(c => c.ProdutoCarrinhos)
            .ThenInclude(c => c.Produto)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}