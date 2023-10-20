using FluentValidation.Results;

namespace CRM.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    
    public virtual bool Validar(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}