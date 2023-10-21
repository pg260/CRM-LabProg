using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class CompraValidator : AbstractValidator<Compra>
{
    public CompraValidator()
    {
        RuleFor(c => c.Quantidade)
            .NotNull()
            .WithMessage("Defina uma quantidade de produtos para a compra.");

        RuleFor(c => c.ValorTotal)
            .NotNull()
            .WithMessage("A sua compra está sem preço, tente novamente mais tarde.");
    }
}