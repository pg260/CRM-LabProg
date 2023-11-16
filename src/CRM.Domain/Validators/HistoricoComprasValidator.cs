using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class HistoricoComprasValidator : AbstractValidator<HistoricoCompras>
{
    public HistoricoComprasValidator()
    {
        RuleFor(c => c.ValorTotal)
            .NotNull()
            .WithMessage("Esse histórico está sem valor, contate o suporte.");

        RuleFor(c => c.MetodoDePagameto)
            .NotEmpty()
            .WithMessage("Selecione um metodo de pagamento.");
    }
}