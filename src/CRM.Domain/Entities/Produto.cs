namespace CRM.Domain.Entities;

public class Produto : BaseEntity
{
    public string Nome { get; set; } = null!;
    public float Valor { get; set; }
    public string Descricao { get; set; } = null!;
}
