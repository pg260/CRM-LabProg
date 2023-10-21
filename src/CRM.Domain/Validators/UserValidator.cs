using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio.")
            .Length(3, 25)
            .WithMessage("O nome precisa ter mais de 3 e menos de 25 caracteres.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("O email não pode ser vazio.")
            .Length(8, 50)
            .WithMessage("O email precisa ter mais de 8 e menos de 50 caracteres.")
            .EmailAddress()
            .WithMessage("Digite um email válido.");

        RuleFor(c => c.Senha)
            .NotEmpty()
            .WithMessage("A senha não pode ser vazia.")
            .Length(6, 20)
            .WithMessage("A senha precisa ter mais de 6 e menos de 20 caracteres.");
    }
}