using CRM.Domain.Validators;
using FluentValidation.Results;

namespace CRM.Domain.Entities;

public class HistoricoCompras : BaseEntity
{
    public int UserId { get; set; }
    public float ValorTotal { get; set; }
    public string MetodoDePagameto { get; set; } = null!;
    public virtual List<Compra> Compras { get; set; } = new();
    public virtual User User { get; set; } = null!;
    
    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new HistoricoComprasValidator().Validate(this);
        return validationResult.IsValid;
    }
}