using Bogus;
using MeuLivroDeReceitas.Comunicacao;

namespace Utilitario.Testes;

public class RequestAlterarSenhaUsuarioBuilder
{
    public static RequestAlterarSenhaJson Contruir(int tamanhoSenha = 10)
    {
        return new Faker<RequestAlterarSenhaJson>()
            .RuleFor(c => c.SenhaAtual, f => f.Internet.Password(10))
            .RuleFor(c => c.NovaSenha, f => f.Internet.Password(tamanhoSenha));
    }
}
