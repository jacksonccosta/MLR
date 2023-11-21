using FluentValidation;
using MeuLivroDeReceitas.Application.UseCases.Usuario;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Exeptions;
using System.Text.RegularExpressions;

namespace MeuLivroDeReceitas.Application;

public class RegistrarUsuarioValidator : AbstractValidator<RequestRegistrarUsuarioJson>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(u => u.Nome).NotEmpty().WithMessage(ResourceMensagensDeErro.NOME_USUARIO_EMBRANCO);
        RuleFor(u => u.Email).NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_USUARIO_EMBRANCO);
        When(u => !string.IsNullOrWhiteSpace(u.Email), () =>
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO);
        });
        RuleFor(u => u.Telefone).NotEmpty().WithMessage(ResourceMensagensDeErro.TELEFONE_USUARIO_EMBRANCO);
        When(u => !string.IsNullOrWhiteSpace(u.Telefone), () =>
        {
            RuleFor(u => u.Telefone).Custom((telefone, contexto) =>
            {
                string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(telefone, padraoTelefone, RegexOptions.None, TimeSpan.FromMilliseconds(100));
                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
                }
            });
        });

        RuleFor(u => u.Senha).SetValidator(new SenhaValidator());
    }
}
