using CRM.Service.Dtos.ProdutoCarrinhoDtos;

namespace CRM.Service.Dtos.CarrinhoDtos;

public class CarrinhoDto
{
    public int UserId { get; set; }
    public float ValorTotal { get; set; }
    public List<ProdutoCarrinhoDto> ProdutoCarrinhos { get; set; } = new();
}