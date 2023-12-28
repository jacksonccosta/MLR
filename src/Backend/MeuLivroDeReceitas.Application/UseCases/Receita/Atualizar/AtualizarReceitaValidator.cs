using FluentValidation;
using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public class AtualizarReceitaValidator : AbstractValidator<RequestReceitaJson>
{
    public AtualizarReceitaValidator()
    {
        RuleFor(x=> x).SetValidator(new ReceitaValidator());
    }
}
