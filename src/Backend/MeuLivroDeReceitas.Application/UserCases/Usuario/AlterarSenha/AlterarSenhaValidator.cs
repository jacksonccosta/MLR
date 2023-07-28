using FluentValidation;
using MeuLivroDeReceitas.Application.UserCases.Usuario;
using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public class AlterarSenhaValidator : AbstractValidator<RequestAlterarSenhaJson>
{
    public AlterarSenhaValidator()
    {
        RuleFor(usuario => usuario.NovaSenha).SetValidator(new SenhaValidator());
    }
}
