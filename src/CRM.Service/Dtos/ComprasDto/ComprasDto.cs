using CRM.Service.Dtos.ProdutoDtos;

namespace CRM.Service.Dtos.ComprasDto;

public class CompraDto
{
    public int Id { get; set; }
    public int Quantidade { get; set; }
    public float ValorTotal { get; set; }
    public ProdutoDto Produto { get; set; } = null!;
}