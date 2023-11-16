using CRM.Domain.Entities;
using CRM.Service.Dtos.PaginatedSearch;

namespace CRM.Service.Dtos.HistoricoComprasDto;

public class BuscarHistoricoComprasDto : PaginatedSearchDto<HistoricoCompras>
{
    public int? UserId { get; set; }
    public float? ValorMaximo { get; set; }
    public string? MetodoDePagamento { get; set; }
    public DateTime? Datainicial { get; set; }
    public DateTime? DataFinal { get; set; }
    
    public override void ApplyFilters(ref IQueryable<HistoricoCompras> query)
    {
        if (UserId != null) query = query.Where(c => c.UserId == UserId);
        if (ValorMaximo != null) query = query.Where(c => c.ValorTotal <= ValorMaximo);
        if (MetodoDePagamento != null) query = query.Where(c => c.MetodoDePagameto.Contains(MetodoDePagamento));
        if (Datainicial != null) query = query.Where(c => c.CriadoEm >= Datainicial);
        if (DataFinal != null) query = query.Where(c => c.CriadoEm <= DataFinal);
    }
    
    public override void ApplyOrdenation(ref IQueryable<HistoricoCompras> query)
    {
        if (DirectionOfOrdenation.Trim().ToLower().Equals("desc"))
        {
            query = OrdenationBy.ToLower().Trim() switch
            {
                "data" => query.OrderByDescending(p => p.CriadoEm),
                "valor" => query.OrderByDescending(p => p.ValorTotal),
                "pagamento" => query.OrderByDescending(p => p.MetodoDePagameto),
                _ => query.OrderByDescending(p => p.CriadoEm)
            };
        }

        query = OrdenationBy.ToLower().Trim() switch
        {
            "data" => query.OrderBy(p => p.CriadoEm),
            "valor" => query.OrderBy(p => p.ValorTotal),
            "pagamento" => query.OrderBy(p => p.MetodoDePagameto),
            _ => query.OrderBy(p => p.CriadoEm)
        };
    }
}