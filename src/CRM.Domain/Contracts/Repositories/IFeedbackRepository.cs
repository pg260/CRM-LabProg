using CRM.Domain.Entities;

namespace CRM.Domain.Contracts.Repositories;

public interface IFeedbackRepository : IBaseRepository<Feedback>
{
    void Criar(Feedback feedback);
    void Excluir(Feedback feedback);
    Task<Feedback?> ObterPorId(int id);
    Task<float> MediaAvaliacoes(int produtoId, int avaliacaoAtual);
    Task<Dictionary<string, int>> ContagemAvaliacaoProduto(int produtoId);
}