namespace CRM.Domain.Entities;

public class User : BaseEntity
{
    public string Nome  { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;

    public virtual Carrinho Carrinho { get; set; } = null!;
}