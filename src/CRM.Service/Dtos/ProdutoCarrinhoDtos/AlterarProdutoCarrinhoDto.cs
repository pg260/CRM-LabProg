namespace CRM.Service.Dtos.ProdutoCarrinhoDtos;

public class AlterarProdutoCarrinhoDto
{
    public int ProdutoId { get; set; }
    public int CarrinhoId { get; set; }
    public int Quantidade { get; set; }
    public float ValorProduto { get; set; }
}