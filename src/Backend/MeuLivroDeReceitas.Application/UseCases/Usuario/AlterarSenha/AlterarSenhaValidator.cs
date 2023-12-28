using FluentValidation;
using MeuLivroDeReceitas.Application.UseCases.Usuario;
using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public class AlterarSenhaValidator : AbstractValidator<RequestAlterarSenhaJson>
{
    public AlterarSenhaValidator()
    {
        RuleFor(usuario => usuario.NovaSenha).SetValidator(new SenhaValidator());
    }
}
