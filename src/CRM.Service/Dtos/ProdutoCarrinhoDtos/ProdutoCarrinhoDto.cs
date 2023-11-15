using CRM.Service.Dtos.ProdutoDtos;

namespace CRM.Service.Dtos.ProdutoCarrinhoDtos;

public class ProdutoCarrinhoDto
{
    public int ProdutoId { get; set; }
    public int CarrinhoId { get; set; }
    public int Quantidade { get; set; }
    public float ValorTotal { get; set; }
    public ProdutoDto Produto { get; set; } = null!;
}