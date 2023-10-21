using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class CarrinhoValidator : AbstractValidator<Carrinho>
{
    public CarrinhoValidator()
    {
        RuleFor(c => c.ValorTotal)
            .NotNull()
            .WithMessage("O valor do carrinho não pode ser nulo.");
    }
}