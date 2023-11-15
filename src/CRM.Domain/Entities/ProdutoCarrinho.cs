namespace CRM.Domain.Entities;

public class ProdutoCarrinho : BaseEntity
{
    public int ProdutoId { get; set; }
    public int CarrinhoId { get; set; }
    public int Quantidade { get; set; }
    public float ValorTotal { get; set; }

    public virtual Produto Produto { get; set; } = null!;
    public virtual Carrinho Carrinho { get; set; } = null!;
}