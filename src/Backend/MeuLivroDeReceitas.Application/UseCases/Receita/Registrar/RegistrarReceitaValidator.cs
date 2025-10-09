using FluentValidation;
using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public class RegistrarReceitaValidator : AbstractValidator<RequestReceitaJson>
{
    public RegistrarReceitaValidator()
    {
        RuleFor(x => x).SetValidator(new ReceitaValidator());
    }
}
