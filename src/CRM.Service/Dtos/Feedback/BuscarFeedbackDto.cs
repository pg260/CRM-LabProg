using CRM.Service.Dtos.PaginatedSearch;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Dtos.Feedback;

public class BuscarFeedbackDto : PaginatedSearchDto<Domain.Entities.Feedback>
{
    public int? UserId { get; set; }
    public int? ProdutoId { get; set; }
    public int? AvaliacaoMinima { get; set; }
    public string? Comentarios { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    
    public override void ApplyFilters(ref IQueryable<Domain.Entities.Feedback> query)
    {
        query = query.Include(c => c.Compra).ThenInclude(c => c.Produto);
        if (UserId != null) query = query.Where(c => c.UserId == UserId);
        if (ProdutoId != null) query = query.Where(c => c.Compra.ProdutoId == ProdutoId);
        if (AvaliacaoMinima != null) query = query.Where(c => c.Avaliacao >= AvaliacaoMinima);
        if (Comentarios != null) query = query.Where(c => c.Comentarios != null && c.Comentarios.Contains(Comentarios));
        if (DataInicial != null) query = query.Where(c => c.CriadoEm >= DataInicial);
        if (DataFinal != null) query = query.Where(c => c.CriadoEm <= DataFinal);
    }
    
    public override void ApplyOrdenation(ref IQueryable<Domain.Entities.Feedback> query)
    {
        if (DirectionOfOrdenation.Trim().ToLower().Equals("desc"))
        {
            query = OrdenationBy.ToLower().Trim() switch
            {
                "avaliacao" => query.OrderByDescending(p => p.Avaliacao),
                "data" or _ => query.OrderByDescending(p => p.CriadoEm)
            };
        }

        query = OrdenationBy.ToLower().Trim() switch
        {
            "avaliacao" => query.OrderBy(p => p.Avaliacao),
            "data" or _ => query.OrderBy(p => p.CriadoEm)
        };
    }
}