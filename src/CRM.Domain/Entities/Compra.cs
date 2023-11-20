using CRM.Domain.Validators;
using FluentValidation.Results;

namespace CRM.Domain.Entities;

public class Compra : BaseEntity
{
    public int HistoricoComprasId { get; set; }
    public int UserId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public float ValorTotal { get; set; }

    public virtual HistoricoCompras HistoricoCompras { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual Produto Produto { get; set; } = null!;
    public virtual List<Feedback>? Feedbacks { get; set; } = new();
    
    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new CompraValidator().Validate(this);
        return validationResult.IsValid;
    }
}