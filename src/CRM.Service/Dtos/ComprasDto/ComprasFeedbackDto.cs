using CRM.Service.Dtos.ProdutoDtos;

namespace CRM.Service.Dtos.ComprasDto;

public class ComprasFeedbackDto
{
    public int Id { get; set; }
    public int Quantidade { get; set; }
    public float ValorTotal { get; set; }
    public ProdutoFeedbackDto Produto { get; set; } = null!;
}