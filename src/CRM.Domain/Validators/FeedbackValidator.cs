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
            .InclusiveBetween(1, 5)
            .WithMessage("Escolha uma opção entre 1 e 5 para a avaliação.");
        
        RuleFor(c => c.Comentarios)
            .MaximumLength(200)
            .WithMessage("Escreva até no máximo 200 caracteres.");
    }
}