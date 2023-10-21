using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class HistoricoComprasValidator : AbstractValidator<HistoricoCompras>
{
    public HistoricoComprasValidator()
    {
        RuleFor(c => c.ValorHistorico)
            .NotNull()
            .WithMessage("Esse histórico está sem valor, contate o suporte.");
    }
}