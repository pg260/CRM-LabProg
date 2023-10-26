namespace CRM.Domain.Entities;

public class Carrinho : BaseEntity
{
    public int UserId { get; set; }
    public float ValorTotal { get; set; }
    
    public virtual User Usuario { get; set; } = null!;
    public virtual List<ProdutoCarrinho> ProdutoCarrinhos { get; set; } = new();
}