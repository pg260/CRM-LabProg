namespace CRM.Service.Dtos.ProdutoDtos;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public float Valor { get; set; }
    public float? Nota { get; set; }
    public Dictionary<string, int> QuantidadeAvaliacoes { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public bool Desativado { get; set; }
    public DateTime CriadoEm { get; set; }
}