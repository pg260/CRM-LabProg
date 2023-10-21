namespace CRM.Domain.Entities;

public class User : BaseEntity
{
    public int? CarrinhoId { get; set; }
    public string Nome  { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;

    public virtual Carrinho? Carrinho { get; set; }
    public virtual List<Compra>? Compras { get; set; } = new();
    public virtual List<HistoricoCompras>? HistoricoCompras { get; set; } = new();
    public virtual List<Feedback> Feedbacks { get; set; } = new();
}