using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class FeedbackValidator : AbstractValidator<Feedback>
{
    public FeedbackValidator()
    {
        RuleFor(c => c.Avaliacao)
            .NotNull()
            .WithMessage("Defina um valor para a avaliação.")
            .GreaterThan(0)
            .WithMessage("Escolha uma opção a partir de 1 para a avaliação.")
            .LessThan(6)
            .WithMessage("Escolha uma opção até 5 para a avaliação.");

        RuleFor(c => c.Comentarios)
            .MaximumLength(200)
            .WithMessage("Escreva até no máximo 200 caracteres.");
    }
}