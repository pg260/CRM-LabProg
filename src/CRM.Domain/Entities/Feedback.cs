using CRM.Domain.Validators;
using FluentValidation.Results;

namespace CRM.Domain.Entities;

public class Feedback : BaseEntity
{
    public int UserId { get; set; }
    public int ProdutoId { get; set; }
    public int CompraId { get; set; }
    public int Avaliacao { get; set; }
    public string? Comentarios { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Produto Produto { get; set; } = null!;
    public virtual Compra Compra { get; set; } = null!;
    
    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new FeedbackValidator().Validate(this);
        return validationResult.IsValid;
    }
}