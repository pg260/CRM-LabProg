using CRM.Domain.Contracts.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Entities.Enums;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infra.Repositories;

public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(BaseDbContext context) : base(context)
    {}

    public void Criar(Feedback feedback) => Context.Feedbacks.Add(feedback);
    public void Excluir(Feedback feedback) => Context.Feedbacks.Remove(feedback);
    public async Task<Feedback?> ObterPorId(int id)
    {
        return await Context.Feedbacks
            .AsNoTracking()
            .Include(c => c.Compra)
            .ThenInclude(c => c.Produto)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<float> MediaAvaliacoes(int produtoId, int avaliacaoAtual)
    {
        var notas = await Context.Feedbacks
            .Where(f => f.ProdutoId == produtoId)
            .Select(f => f.Avaliacao)
            .ToListAsync();
        
        notas.Add(avaliacaoAtual);
        var media = notas.Average();
        return (float)media;
    }

    public async Task<Dictionary<string, int>> ContagemAvaliacaoProduto(int produtoId)
    {
        var todosNiveisAvaliacao = Enum.GetValues(typeof(EAvaliacaoTipo)).Cast<EAvaliacaoTipo>();
        
        var result = await Context.Feedbacks
            .Where(f => f.ProdutoId == produtoId)
            .GroupBy(f => f.Avaliacao)
            .Select(g => new { Avaliacao = (EAvaliacaoTipo)g.Key, Count = g.Count() })
            .OrderByDescending(f => f.Count)
            .ToDictionaryAsync(x => x.Avaliacao.ToString(), x => x.Count);
        
        var resultadoFinal = todosNiveisAvaliacao.ToDictionary(
            nivel => nivel.ToString(),
            nivel => result.ContainsKey(nivel.ToString()) ? result[nivel.ToString()] : 0);

        return resultadoFinal;
    }
}