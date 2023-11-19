using CRM.Domain.Entities;
using CRM.Service.Dtos.ComprasDto;

namespace CRM.Service.Dtos.HistoricoComprasDto;

public class HistoricoComprasDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public float ValorTotal { get; set; }
    public string MetodoDePagameto { get; set; } = null!;
    public DateTime CriadoEm { get; set; }

    public List<ComprasFeedbackDto> Compras { get; set; } = new();
}