using CRM.Domain.Validators;
using FluentValidation.Results;

namespace CRM.Domain.Entities;

public class Carrinho : BaseEntity
{
    public int UserId { get; set; }
    public float ValorTotal { get; set; }
    
    public virtual User Usuario { get; set; } = null!;
    public virtual List<ProdutoCarrinho> ProdutoCarrinhos { get; set; } = new();
    
    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new CarrinhoValidator().Validate(this);
        return validationResult.IsValid;
    }
}