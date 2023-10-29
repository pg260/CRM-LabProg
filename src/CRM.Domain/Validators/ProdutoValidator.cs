using CRM.Domain.Entities;
using FluentValidation;

namespace CRM.Domain.Validators;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Digite um nome para o produto.")
            .Length(3, 30)
            .WithMessage("O nome deve conter no mínimo 3 e no máximo 30 caracteres.");

        RuleFor(c => c.Descricao)
            .NotEmpty()
            .WithMessage("O produto deve conter uma descrição.")
            .Length(5, 50)
            .WithMessage("A descrição deve conter no mínimo 5 e no máximo 50 caracteres.");

        RuleFor(c => c.Valor)
            .NotNull()
            .WithMessage("Defina um valor para o produto.")
            .GreaterThan(0)
            .WithMessage("O valor precisa ser maior que 0.");

        RuleFor(c => c.Cidade)
            .NotEmpty()
            .WithMessage("Escolha a cidade em que o produto de encontra.")
            .MaximumLength(100)
            .WithMessage("O nome da cidade pode conter apenas 100 caracteres.");
        
        RuleFor(c => c.Estado)
            .NotEmpty()
            .WithMessage("Escolha o estado em que o produto de encontra.")
            .MaximumLength(100)
            .WithMessage("O nome do estado pode conter apenas 100 caracteres.");
    }
}