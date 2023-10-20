namespace CRM.Domain.Entities;

public class HistoricoCompras : BaseEntity
{
    public int UserId { get; set; }
    public float ValorHistorico { get; set; }
    
    public virtual List<Compra> Compras { get; set; } = new();
    public virtual User User { get; set; } = null!;
}