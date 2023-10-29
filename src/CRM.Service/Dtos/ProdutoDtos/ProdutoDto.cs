namespace CRM.Service.Dtos.ProdutoDtos;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public float Valor { get; set; }
    public string Descricao { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
}