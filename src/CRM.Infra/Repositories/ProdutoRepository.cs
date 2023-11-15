using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(BaseDbContext context) : base(context)
    { }

    public void Criar(Produto produto) => Context.Produtos.Add(produto);
    public void Editar(Produto produto) => Context.Produtos.Update(produto);
    public async Task<Produto?> ObterPorId(int id)
    {
        return await Context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}