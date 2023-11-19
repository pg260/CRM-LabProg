namespace CRM.Domain.Entities;

public class Produto : BaseEntity
{
    public int UserId { get; set; }
    public string Nome { get; set; } = null!;
    public float Valor { get; set; }
    public string Descricao { get; set; } = null!;
    public float? Nota { get; set; }
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public bool Desativado { get; set; }

    public virtual List<ProdutoCarrinho> ProdutoCarrinhos { get; set; } = new();
    public virtual List<Feedback> Feedbacks { get; set; } = new();
    public virtual User User { get; set; } = null!;
}
